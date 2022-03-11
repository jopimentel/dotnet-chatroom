using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	/// 
	/// </summary>
	public class StockQuoteHandler : IHandler<RequestStockQuote>
	{
		/// <summary>
		/// 
		/// </summary>
		public string Queue => "bot::stock.quote.in";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="model"></param>
		/// <param name="arguments"></param>
		/// <returns></returns>
		public async Task HandleAsync(RequestStockQuote data, IModel model, BasicDeliverEventArgs arguments)
		{
			CancellationTokenSource tokenSource = new(60000);

			string url = $"https://stooq.com/q/l/?s={data.StockCode}&f=sd2t2ohlcv&h&e=csv";
			using HttpClient client = new();
			using HttpResponseMessage response = await client.GetAsync(url, tokenSource.Token);

			if (response.StatusCode != HttpStatusCode.OK)
				return;

			using Stream content = await response.Content.ReadAsStreamAsync(tokenSource.Token);
			//using MemoryStream memoryStream = new();

			//content.CopyTo(memoryStream);

			string filename = GetFileName(response) ?? data.StockCode;
			//ObjectId fileId = await _fileRepository.SaveToGridFSAsync(data.Filename, filename, content, tokenSource.Token);

			StooqResponse fileContent = await ReadStreamAsync(content, tokenSource.Token);

			string body = JsonSerializer.Serialize(fileContent);
			byte[] message = Encoding.UTF8.GetBytes(body);

			IBasicProperties properties = model.CreateBasicProperties();
			properties.CorrelationId = Guid.NewGuid().ToString();

			model.BasicPublish(exchange: "", routingKey: arguments.BasicProperties.ReplyTo, mandatory: false, properties, message);
			model.BasicAck(arguments.DeliveryTag, multiple: false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		private static string GetFileName(HttpResponseMessage response)
		{
			// TODO: move to extensión method
			bool exists = response.Content.Headers.TryGetValues("Content-Disposition", out IEnumerable<string> values);

			if (!exists)
				return null;

			string disposition = values.FirstOrDefault();
			ContentDisposition contentDisposition = new(disposition);

			if (contentDisposition.DispositionType != "attachment")
				return null;

			return contentDisposition.FileName;
		}

		private async Task<StooqResponse> ReadStreamAsync(Stream stream, CancellationToken cancellationToken = default)
		{
			using StreamReader reader = new(stream);

			string fileContent = await reader.ReadToEndAsync();

			cancellationToken.ThrowIfCancellationRequested();

			string data = fileContent.Split('\n')[1];
			string[] values = data.Split(',');

			StooqResponse response = new()
			{
				Symbol = values[0],
				Date = DateTime.Parse(values[1]),
				Time = TimeSpan.Parse(values[2]),
				Open = decimal.Parse(values[3]),
				High = decimal.Parse(values[4]),
				Low = decimal.Parse(values[5]),
				Close = decimal.Parse(values[6]),
				Volume = long.Parse(values[7])
			};

			return response;
		}
	}
}

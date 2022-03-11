using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Net.Mime;
using Dotnet.Chatroom.Bot.Repository;
using MongoDB.Bson;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Dotnet.Chatroom.Bot.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class StocksController : ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly ILogger<StocksController> _logger;
		private readonly IFileRepository _fileRepository;
		private readonly IModel _model;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		public StocksController(ILogger<StocksController> logger, IFileRepository fileRepository, IModel model)
		{
			_logger = logger;
			_fileRepository = fileRepository;
			_model = model;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stockCode"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost("{stockCode}")]
		public async Task<IActionResult> RequestQuoteByStockCodeAsync(string stockCode, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Requesting the stock quote of {stockCode}", stockCode);

			string newFilename = Guid.NewGuid().ToString();
			string url = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";
			using HttpClient client = new();

			try
			{
				using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

				if (response.StatusCode != HttpStatusCode.OK)
					return StatusCode((int)response.StatusCode, response.Content);

				using Stream content = await response.Content.ReadAsStreamAsync(cancellationToken);

				string filename = GetFileName(response) ?? stockCode;
				ObjectId fileId = await _fileRepository.SaveToGridFSAsync(newFilename, filename, content, cancellationToken);

				RequestStockQuote request = new()
				{
					Id = Guid.NewGuid().ToString(),
					FileId = fileId.ToString(),
					StockCode = stockCode,
					Filename = newFilename,
					Length = content.Length
				};

				string body = JsonSerializer.Serialize(request);
				byte[] message = Encoding.UTF8.GetBytes(body);

				IBasicProperties properties = _model.CreateBasicProperties();
				properties.CorrelationId = request.Id;
				properties.ReplyTo = "bot::stock.quote.out";

				_model.BasicPublish(exchange: "", routingKey: "bot::stock.quote.in", mandatory: false, properties, message);

				return Ok(request);
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while requesting the stock quote of {stockCode}:", stockCode);
				_logger.LogError("{message}", exception.GetBaseException().Message);

				return StatusCode((int)HttpStatusCode.InternalServerError, exception);
			}
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
	}
}
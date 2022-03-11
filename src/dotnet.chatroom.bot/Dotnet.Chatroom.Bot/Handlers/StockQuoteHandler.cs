using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
		public Task HandleAsync(RequestStockQuote data, IModel model, BasicDeliverEventArgs arguments)
		{
			try
			{
				string body = JsonSerializer.Serialize(new { Quote = 251 });
				byte[] message = Encoding.UTF8.GetBytes(body);

				IBasicProperties properties = model.CreateBasicProperties();
				properties.CorrelationId = Guid.NewGuid().ToString();

				model.BasicPublish(exchange: "", routingKey: arguments.BasicProperties.ReplyTo, mandatory: false, properties, message);
				model.BasicAck(arguments.DeliveryTag, multiple: false);
			}
			catch (Exception)
			{

			}

			return Task.CompletedTask;
		}
		private void ReadStream(Stream stream)
		{
			using SpreadsheetDocument excel = SpreadsheetDocument.Open(stream, isEditable: true);
			WorkbookPart workbook = excel.WorkbookPart;
			SheetData sheet = GetSheet(workbook);
			Row row = sheet.Elements<Row>().FirstOrDefault(r => r.RowIndex == 2);
			Cell[] cells = row.Elements<Cell>().ToArray();

			var a = new
			{
				Low = GetValue(cells, 0)
			};
		}

		private SheetData GetSheet(WorkbookPart w)
		{
			return null;
		}

		private string GetValue(Cell[] c, int index)
		{
			return null;
		}
	}
}

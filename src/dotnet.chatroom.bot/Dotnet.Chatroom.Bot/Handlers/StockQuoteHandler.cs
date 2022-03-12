using Dotnet.Chatroom.Bot.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;

namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	///	The intention of this handler is to receive messages sent to the stock quote input queue.
	/// </summary>
	/// <remarks>
	///		<para>When a message arrives, the handler calls the stooq api to get the stock quote value.</para>
	///		<para>Implements the <see cref="IHandler{T}"/> interface.</para>
	///	</remarks>
	public class StockQuoteHandler : IHandler<RequestStockQuote>
	{
		/// <summary>
		/// Refers to the queue the handler is watching.
		/// </summary>
		public string Queue => Environment.StockQuoteIn;
		/// <summary>
		/// Allows to save the csv obtained from the stooq api in MongoDB GridFS.
		/// </summary>
		private readonly IFileService _fileService;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Stock"/> entity. 
		/// </summary>
		private readonly IStockService _stockService;
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<StockQuoteHandler> _logger;

		/// <summary>
		/// Initializes a new instance of <see cref="StockQuoteHandler"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="fileService">The <see cref="IFileService"/> used to save files to GridFS.</param>
		/// <param name="stockService">The <see cref="IStockService"/> used to manage the <see cref="Stock"/> entity.</param>
		public StockQuoteHandler(ILogger<StockQuoteHandler> logger, IFileService fileService, IStockService stockService)
		{
			_logger = logger;
			_fileService = fileService;
			_stockService = stockService;
		}

		/// <summary>
		/// Handles the received message and makes the request to the stooq api.
		/// </summary>
		/// <param name="data">The content of the message passed through RabbitMQ.</param>
		/// <param name="model"><see cref="IModel"/> object used to acknowledge the message.</param>
		/// <param name="arguments">Contains all the information about the delivered message.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		public async Task HandleAsync(RequestStockQuote data, IModel model, BasicDeliverEventArgs arguments, CancellationToken cancellationToken = default)
		{
			try
			{
				string correlationId = arguments.BasicProperties.CorrelationId;
				string replyTo = arguments.BasicProperties.ReplyTo;
				ulong deliveryTag = arguments.DeliveryTag;

				_logger.LogInformation("Getting {stockCode} stock quote from the stooq api", data.StockCode);

				using HttpResponseMessage response = await GetStockAsync(data.StockCode, cancellationToken);

				if (response.StatusCode != HttpStatusCode.OK)
					return;

				using Stream content = await response.Content.ReadAsStreamAsync(cancellationToken);

				// Saves the result to GridFS (for diagnostic purpose)
				string filename = response.GetFilename() ?? data.StockCode;
				await _fileService.SaveToGridFSAsync(correlationId, filename, content, cancellationToken);

				// Reads the response and save it (for diagnostic purpose)
				StooqResponse fileContent = await ReadStreamAsync(content, cancellationToken);
				Stock stock = await _stockService.AddAsync(correlationId, fileContent, cancellationToken);

				// Acknowledges the message and pubish the stock quote information 
				// to the specified reply to queue
				await model.PublishAsync(stock, routingKey: replyTo, cancellationToken);
				model.BasicAck(deliveryTag, multiple: false);

				_logger.LogInformation("{stockCode} stock quote published to {replyTo}", data.StockCode, replyTo);
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while requesting the stock quote of {stockCode}:", data.StockCode);
				_logger.LogError("{message}", exception.GetBaseException().Message);
			}
		}

		/// <summary>
		/// Makes the request to the stooq api for a given stock code.
		/// </summary>
		/// <remarks>The stock quote information comes in a csv file.</remarks>
		/// <param name="stockCode">The unique identifier of the stock quote.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the <see cref="HttpResponseMessage"/> object which contains the stock quote information.
		/// </returns>
		private static async Task<HttpResponseMessage> GetStockAsync(string stockCode, CancellationToken cancellationToken)
		{
			string url = string.Format(Environment.StooqApi, stockCode);
			using HttpClient client = new();

			return await client.GetAsync(url, cancellationToken);
		}

		/// <summary>
		/// Reads the stream which contains the stock quote information.
		/// </summary>
		/// <param name="stream">Refers to the obtained csv file.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the content of the stooq api response.
		/// </returns>
		/// <exception cref="InvalidOperationException"/>
		private async Task<StooqResponse> ReadStreamAsync(Stream stream, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Reading stream that contains the stooq api response");

			// Sets to 0 the position of the stream to restart it
			// to the start point for the read process 
			stream.Position = 0;

			using StreamReader reader = new(stream);
			string fileContent = await reader.ReadToEndAsync();

			cancellationToken.ThrowIfCancellationRequested();

			if (string.IsNullOrWhiteSpace(fileContent))
				throw new InvalidOperationException();

			// Gets the second line of the file and parse the content
			string data = fileContent.Split('\n')[1];
			string[] stock = data.Split(',');

			return new StooqResponse()
			{
				Symbol = stock[0],
				Date = DateTime.Parse(stock[1]),
				Time = TimeSpan.Parse(stock[2]),
				Open = decimal.Parse(stock[3]),
				High = decimal.Parse(stock[4]),
				Low = decimal.Parse(stock[5]),
				Close = decimal.Parse(stock[6]),
				Volume = long.Parse(stock[7])
			};
		}
	}
}

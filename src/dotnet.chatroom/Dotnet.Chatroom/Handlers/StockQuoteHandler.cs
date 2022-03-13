using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Chatroom
{
	/// <summary>
	///	The intention of this handler is to receive messages sent to the stock quote output queue.
	/// </summary>
	/// <remarks>
	///		<para>When a message arrives, the handler sends the received stock quote to the corresponding audience.</para>
	///		<para>Implements the <see cref="IHandler{T}"/> interface.</para>
	///	</remarks>
	public class StockQuoteHandler : IHandler<Stock>
	{
		/// <summary>
		/// Refers to the queue the handler is watching.
		/// </summary>
		public string Queue => Environment.StockQuoteOut;
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<StockQuoteHandler> _logger;
		/// <summary>
		/// 
		/// </summary>
		private readonly HubConnection _hub;

		/// <summary>
		/// Initializes a new instance of <see cref="StockQuoteHandler"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		public StockQuoteHandler(ILogger<StockQuoteHandler> logger)
		{
			_logger = logger;
			_hub = BuilHubConnection(Environment.MessageHub);
		}

		/// <summary>
		/// Handles the received message and makes the request to the stooq api.
		/// </summary>
		/// <param name="data">The content of the message passed through RabbitMQ.</param>
		/// <param name="model"><see cref="IModel"/> object used to acknowledge the message.</param>
		/// <param name="arguments">Contains all the information about the delivered message.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		public async Task HandleAsync(Stock data, IModel model, BasicDeliverEventArgs arguments, CancellationToken cancellationToken = default)
		{
			ulong deliveryTag = arguments.DeliveryTag;

			_logger.LogInformation("Getting the quote of the {symbol} stock", data.Symbol);

			try
			{
				if (_hub.State != HubConnectionState.Connected)
					await _hub.StartAsync(cancellationToken);

				await _hub.InvokeCoreAsync("InvokeStock", new[] { data }, cancellationToken);
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while getting the stock quote:");
				_logger.LogError("{message}", exception.GetBaseException().Message);
			}

			model.BasicAck(deliveryTag, multiple: false);
		}

		private static HubConnection BuilHubConnection(string url)
		{
			return new HubConnectionBuilder()
				.WithUrl(url)
				.ConfigureLogging(logging => logging.AddConsole())
				.WithAutomaticReconnect()
				.Build();
		}
	}
}

using Dotnet.Chatroom.Bot.Repository;
using RabbitMQ.Client;

namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Provides a set of methods that allows to manage the <see cref="Request"/> entity.
	/// </summary>
	/// <remarks>This class implements the <see cref="IRequestService"/> interface.</remarks>
	public class RequestService : IRequestService
	{
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Request"/> entity. 
		/// </summary>
		private readonly IRequestRepository _requestRepository;
		/// <summary>
		/// The <see cref="IModel"/> object used to publish messages to RabbitMQ.
		/// </summary>
		private readonly IModel _model;
		/// <summary>
		/// Contains the data provided by the current context.
		/// </summary>
		private readonly IApplicationContext _context;

		/// <summary>
		/// Initializes a new instance of <see cref="RequestService"/> type.
		/// </summary>
		/// <param name="context">Data obtained from the current context.</param>
		/// <param name="requestRepository">The <see cref="IRequestRepository"/> used to manage the <see cref="Request"/> entity.</param>
		/// <param name="model"><see cref="IModel"/> object used to publish a message requesting a stock quote.</param>
		public RequestService(IApplicationContext context, IRequestRepository requestRepository, IModel model)
		{
			_requestRepository = requestRepository;
			_model = model;
			_context = context;
		}

		/// <summary>
		/// Adds a <see cref="Request"/> to the database and publish a message requesting a stock quote.
		/// </summary>
		/// <param name="dataTransferObject">Contains the information provided by the user. This object is used to build the <see cref="Request"/>.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the id of the request to be processed.
		/// </returns>
		public async Task<string> AddAsync(RequestStockQuote dataTransferObject, CancellationToken cancellationToken = default)
		{
			string routingKey = Environment.StockQuoteIn;
			string replayTo = Environment.StockQuoteOut;

			// Publish the message requesting the stock quote
			string correlationId = await _model.PublishAsync(dataTransferObject, routingKey, replayTo, cancellationToken);

			Request request = new()
			{
				Id = correlationId,
				Command = new Command()
				{
					Id = Guid.NewGuid().ToString(),
					Action = dataTransferObject.Action,
					Value = dataTransferObject.StockCode
				},
				Date = DateTimeOffset.UtcNow,
				Audience = _context.Audience
			};

			await _requestRepository.AddAsync(request, cancellationToken);

			return correlationId;
		}
	}
}
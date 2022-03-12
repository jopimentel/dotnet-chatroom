using Dotnet.Chatroom.Bot.Repository;

namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Provides a set of methods that allows to manage the <see cref="Stock"/> entity.
	/// </summary>
	/// <remarks>This class implements the <see cref="IStockService"/> interface.</remarks>
	public class StockService : IStockService
	{
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Request"/> entity. 
		/// </summary>
		private readonly IRequestRepository _requestRepository;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Stock"/> entity. 
		/// </summary>
		private readonly IStockRepository _stockRepository;

		/// <summary>
		/// Initializes a new instance of <see cref="StockService"/> type.
		/// </summary>
		/// <param name="stockRepository">The <see cref="IStockRepository"/> used to manage the <see cref="Stock"/> entity.</param>
		/// <param name="requestRepository">The <see cref="IRequestRepository"/> used to manage the <see cref="Request"/> entity.</param>
		public StockService(IStockRepository stockRepository, IRequestRepository requestRepository)
		{
			_stockRepository = stockRepository;
			_requestRepository = requestRepository;
		}

		/// <summary>
		/// Adds a <see cref="Stock"/> to the database.
		/// </summary>
		/// <remarks>The correlationId must be the one provided by the message requesting the creation of the stock.</remarks>
		/// <param name="correlationId">The primary key with which the entity will be created.</param>
		/// <param name="dataTransferObject">Contains the information obtained from the stooq api. This object is used to build the <see cref="Stock"/>.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the created stock.
		/// </returns>
		public async Task<Stock> AddAsync(string correlationId, StooqResponse dataTransferObject, CancellationToken cancellationToken = default)
		{
			Request request = await _requestRepository.GetByIdAsync(correlationId, cancellationToken);
			Stock stock = new()
			{
				Id = correlationId,
				Symbol = dataTransferObject.Symbol,
				Date = dataTransferObject.Date,
				Time = dataTransferObject.Time,
				Open = dataTransferObject.Open,
				High = dataTransferObject.High,
				Low = dataTransferObject.Low,
				Close = dataTransferObject.Close,
				Volume = dataTransferObject.Volume,
				Request = request
			};

			await _stockRepository.AddAsync(stock, cancellationToken);

			return stock;
		}
	}
}
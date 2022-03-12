using MongoDB.Driver;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class StockRepository : IStockRepository
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IMongoDatabase<Stock> _mongodb;
		/// <summary>
		/// 
		/// </summary>
		private readonly string _collection = "stocks";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mongodb"></param>
		public StockRepository(IMongoDatabase<Stock> mongodb)
		{
			_mongodb = mongodb;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stock"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task AddAsync(Stock stock, CancellationToken cancellationToken = default)
		{
			IMongoCollection<Stock> stocks = _mongodb.GetCollection<Stock>(_collection);

			return stocks.InsertOneAsync(stock, cancellationToken: cancellationToken);
		}
	}
}

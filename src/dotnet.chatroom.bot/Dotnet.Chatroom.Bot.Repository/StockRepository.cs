namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// Abstracts the access to the database where the <see cref="Stock"/> will be saved and query.
	/// </summary>
	/// <remarks>This class implements the <see cref="IStockRepository"/> interface.</remarks>
	public class StockRepository : IStockRepository
	{
		/// <summary>
		/// Represents the connection to the MongoDB database to be used to manage the <see cref="Stock"/> entity.
		/// </summary>
		private readonly IMongoDatabase<Stock> _mongodb;
		/// <summary>
		/// Refers to the name of the collection to be used to store the information related to the <see cref="Stock"/> entity.
		/// </summary>
		private readonly string _collection = "stocks";

		/// <summary>
		/// Initializes a new intance of <see cref="StockRepository"/> type.
		/// </summary>
		/// <param name="mongodb"></param>
		public StockRepository(IMongoDatabase<Stock> mongodb)
		{
			_mongodb = mongodb;
		}

		/// <summary>
		/// Adds a <see cref="Stock"/> to the database.
		/// </summary>
		/// <param name="stock">The <see cref="Stock"/> object to be added to the database.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		public Task AddAsync(Stock stock, CancellationToken cancellationToken = default)
		{
			return _mongodb.GetCollection<Stock>(_collection).InsertOneAsync(stock, cancellationToken: cancellationToken);
		}
	}
}

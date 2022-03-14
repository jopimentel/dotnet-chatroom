using MongoDB.Driver;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// Abstracts the access to the database where the <see cref="Request"/> will be saved and query.
	/// </summary>
	/// <remarks>This class implements the <see cref="IRequestRepository"/> interface.</remarks>
	public class RequestRepository : IRequestRepository
	{
		/// <summary>
		/// Represents the connection to the MongoDB database to be used to manage the <see cref="Request"/> entity.
		/// </summary>
		private readonly IMongoDatabase<Stock> _mongodb;
		/// <summary>
		/// Refers to the name of the collection to be used to store the information related to the <see cref="Request"/> entity.
		/// </summary>
		private readonly string _collection = "requests";

		/// <summary>
		/// Initializes a new instance of <see cref="RequestRepository"/> type.
		/// </summary>
		/// <param name="mongodb">The connection to the MongoDB database to be used to manage the <see cref="Request"/> entity.</param>
		public RequestRepository(IMongoDatabase<Stock> mongodb)
		{
			_mongodb = mongodb;
		}

		/// <summary>
		/// Adds a <see cref="Request"/> to the database.
		/// </summary>
		/// <param name="request">Contains the information provided by the user to get a stock quote.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		public Task AddAsync(Request request, CancellationToken cancellationToken = default)
		{
			return _mongodb.GetCollection<Request>(_collection).InsertOneAsync(request, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Get a single request by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the desired request.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the <see cref="Request"/> entity.
		/// </returns>
		public Task<Request> GetByIdAsync(string id, CancellationToken cancellationToken = default)
		{
			return _mongodb.GetCollection<Request>(_collection).Find(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
		}
	}
}

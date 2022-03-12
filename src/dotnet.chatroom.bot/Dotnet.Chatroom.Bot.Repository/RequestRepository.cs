using MongoDB.Driver;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class RequestRepository : IRequestRepository
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IMongoDatabase<Stock> _mongodb;
		/// <summary>
		/// 
		/// </summary>
		private readonly string _collection = "requests";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mongodb"></param>
		public RequestRepository(IMongoDatabase<Stock> mongodb)
		{
			_mongodb = mongodb;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task AddAsync(Request request, CancellationToken cancellationToken = default)
		{
			IMongoCollection<Request> requests = _mongodb.GetCollection<Request>(_collection);

			return requests.InsertOneAsync(request, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<Request> GetByIdAsync(string id, CancellationToken cancellationToken = default)
		{
			IMongoCollection<Request> requests = _mongodb.GetCollection<Request>(_collection);

			return requests.Find(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
		}
	}
}

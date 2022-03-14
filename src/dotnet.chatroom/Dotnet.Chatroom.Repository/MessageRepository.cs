using MongoDB.Driver;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageRepository : IMessageRepository
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IMongoDatabase<Message> _mongodb;
		/// <summary>
		/// 
		/// </summary>
		private readonly string _collection = "messages";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public MessageRepository(IMongoDatabase<Message> mongodb)
		{
			_mongodb = mongodb;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task AddAsync<T>(Message<T> message, CancellationToken cancellationToken = default)
		{
			IMongoCollection<Message> messages = _mongodb.GetCollection<Message>(_collection);

			return messages.InsertOneAsync(message, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<List<Message<object>>> GetByAudienceAsync(string audience, int itemsPerPage = 50, CancellationToken cancellationToken = default)
		{
			IMongoCollection<Message<object>> messages = _mongodb.GetCollection<Message<object>>(_collection);

			return messages
				.Find(m => m.Audience == audience)
				.SortByDescending(m => m.Created)
				.Limit(itemsPerPage)
				.ToListAsync(cancellationToken);
		}
	}
}

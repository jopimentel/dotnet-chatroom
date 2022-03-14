using MongoDB.Driver;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Abstracts the access to the database where the <see cref="Message"/> will be saved and query.
	/// </summary>
	/// <remarks>This class implements the <see cref="IMessageRepository"/> interface.</remarks>
	public class MessageRepository : IMessageRepository
	{
		/// <summary>
		/// Represents the connection to the MongoDB database to be used to manage the <see cref="Message"/> entity.
		/// </summary>
		private readonly IMongoDatabase<Message> _mongodb;
		/// <summary>
		/// Refers to the name of the collection to be used to store the information related to the <see cref="Message"/> entity.
		/// </summary>
		private readonly string _collection = "messages";

		/// <summary>
		/// Initializes a new instance of <see cref="MessageRepository"/> type.
		/// </summary>
		/// <param name="mongodb">The connection to the MongoDB database to be used to manage the <see cref="User"/> entity.</param>
		public MessageRepository(IMongoDatabase<Message> mongodb)
		{
			_mongodb = mongodb;
		}

		/// <summary>
		/// Allows to save a message to the database.
		/// </summary>
		/// <param name="message">The message to be added.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task{TResult}"/> that indicates the completation of the operation.</returns>
		public Task AddAsync<T>(Message<T> message, CancellationToken cancellationToken = default)
		{
			IMongoCollection<Message> messages = _mongodb.GetCollection<Message>(_collection);

			return messages.InsertOneAsync(message, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Gets the messages produced by the given audience.
		/// </summary>
		/// <remarks>By default the service just query the top 50.</remarks>
		/// <param name="audience">The unique identifier of the audience.</param>
		/// <param name="itemsPerPage">Amount of messages to be returned.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested messages produced by the given audience.
		/// </returns>
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

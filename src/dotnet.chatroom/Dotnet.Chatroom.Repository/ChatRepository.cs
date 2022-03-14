namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Abstracts the access to the database where the <see cref="Chat"/> will be saved and query.
	/// </summary>
	/// <remarks>This class implements the <see cref="IChatRepository"/> interface.</remarks>
	public class ChatRepository : IChatRepository
	{
		/// <summary>
		/// Represents the connection to the database.
		/// </summary>
		private readonly ChatroomContext _context;

		/// <summary>
		/// Initializes a new instance of <see cref="ChatRepository"/> type.
		/// </summary>
		/// <param name="context">The object used to access to the database.</param>
		public ChatRepository(ChatroomContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Creates a new chat.
		/// </summary>
		/// <param name="chat">The object with the information used to create the chat.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the amount of entities affected by the operation.
		/// </returns>
		public Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default)
		{
			_context.Chats.Add(chat);

			return _context.SaveChangesAsync(cancellationToken);
		}
	}
}

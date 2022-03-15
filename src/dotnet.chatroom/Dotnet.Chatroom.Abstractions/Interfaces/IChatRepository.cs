namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Defines the abstraction to the database where the <see cref="Chat"/> will be saved and query.
	/// </summary>
	public interface IChatRepository
	{
		/// <summary>
		/// Creates a new chat.
		/// </summary>
		/// <param name="chat">The object with the information used to create the chat.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the amount of entities affected by the operation.
		/// </returns>
		Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default);
	}
}

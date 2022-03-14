namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// Defines a set of methods that allows to manage the <see cref="Chat"/> entity.
	/// </summary>
	/// <remarks>This definition is intended to be implementented in a business specific class.</remarks>
	public interface IChatService
	{
		/// <summary>
		/// Creates a new chat.
		/// </summary>
		/// <remarks>If the <see cref="Chat.Users"/> collection is provided then, the users will be attached to the chat.</remarks>
		/// <param name="chat">The object with the information used to create the chat.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the amount of entities affected by the operation.
		/// </returns>
		Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default);

		/// <summary>
		/// Allows to save to the database a message from a specific chat.
		/// </summary>
		/// <param name="message">The message to be added.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task{TResult}"/> that indicates the completation of the operation.</returns>
		Task SaveMessageAsync<T>(Message<T> message, CancellationToken cancellationToken = default);

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
		Task<List<Message<object>>> GetByAudienceAsync(string audience, int itemsPerPage = 50, CancellationToken cancellationToken = default);
	}
}

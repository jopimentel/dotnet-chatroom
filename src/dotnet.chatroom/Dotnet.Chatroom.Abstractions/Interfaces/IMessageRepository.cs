namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Defines the abstraction to the database where the <see cref="Message"/> will be saved and query.
	/// </summary>
	public interface IMessageRepository
	{
		/// <summary>
		/// Allows to save a message to the database.
		/// </summary>
		/// <param name="message">The message to be added.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task{TResult}"/> that indicates the completation of the operation.</returns>
		Task AddAsync<T>(Message<T> message, CancellationToken cancellationToken = default);

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

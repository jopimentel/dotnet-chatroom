namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// Defines the abstraction to the database where the <see cref="Request"/> will be saved and query.
	/// </summary>
	public interface IRequestRepository
	{
		/// <summary>
		/// Adds a <see cref="Request"/> to the database.
		/// </summary>
		/// <param name="request">Contains the information provided by the user to get a stock quote.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		Task AddAsync(Request request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a single request by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the desired request.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the <see cref="Request"/> entity.
		/// </returns>
		Task<Request> GetByIdAsync(string id, CancellationToken cancellationToken = default);
	}
}

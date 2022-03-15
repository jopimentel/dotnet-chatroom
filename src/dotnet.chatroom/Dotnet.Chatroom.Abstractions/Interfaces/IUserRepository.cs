namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Defines the abstraction to the database where the <see cref="User"/> will be saved and query.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Adds a <see cref="User"/> to the database.
		/// </summary>
		/// <param name="user">Contains the information of the user to be added to the database.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the amount of entities affected by the operation.
		/// </returns>
		Task<int> AddAsync(User user, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets the chats the given user belongs to.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested chats.
		/// </returns>
		Task<List<Chat>> GetChatsAsync(string userId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a single user by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested user.
		/// </returns>
		Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a single user by its username or email address.
		/// </summary>
		/// <param name="usernameOrEmail">The username or email address of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested user.
		/// </returns>
		Task<User> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken = default);
	}
}

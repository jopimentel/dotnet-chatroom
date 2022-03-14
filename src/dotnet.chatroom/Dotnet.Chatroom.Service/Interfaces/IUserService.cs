namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// Defines a set of methods that allows to manage the <see cref="User"/> entity.
	/// </summary>
	/// <remarks>This definition is intended to be implementented in a business specific class.</remarks>
	public interface IUserService
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
	}
}

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<int> AddAsync(User user, CancellationToken cancellationToken = default);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<List<Chat>> GetChatsAsync(string userId, CancellationToken cancellationToken = default);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);
	}
}

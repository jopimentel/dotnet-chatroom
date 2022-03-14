using Dotnet.Chatroom.Repository;
using System.Text;

namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// 
	/// </summary>
	public class UserService : IUserService
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IUserRepository _userRespository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userRespository"></param>
		public UserService(IUserRepository userRespository)
		{
			_userRespository = userRespository;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<int> AddAsync(User user, CancellationToken cancellationToken = default)
		{
			// TODO: Move to a service to provide the flexibility DI provides
			byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
			string password = Convert.ToBase64String(passwordBytes);

			user.Password = password;
			user.Created = DateTimeOffset.UtcNow;

			return _userRespository.AddAsync(user, cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<List<Chat>> GetChatsAsync(string userId, CancellationToken cancellationToken = default)
		{
			return _userRespository.GetChatsAsync(userId, cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
		{
			return _userRespository.GetByIdAsync(id, cancellationToken);
		}
	}
}
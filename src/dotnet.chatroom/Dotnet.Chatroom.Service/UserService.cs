using Dotnet.Chatroom.Repository;
using Microsoft.Extensions.Logging;

namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// Provides a set of methods that allows to manage the <see cref="User"/> entity.
	/// </summary>
	/// <remarks>This class implements the <see cref="IUserService"/> interface.</remarks>
	public class UserService : IUserService
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<UserService> _logger;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="User"/> entity. 
		/// </summary>
		private readonly IUserRepository _userRespository;
		/// <summary>
		/// Allows to encrypt and decrypt a text.
		/// </summary>
		private readonly IEncryptor _encryptor;

		/// <summary>
		/// Initializes a new instance of <see cref="UserService"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="userRespository">The <see cref="IUserRepository"/> used to manage the <see cref="User"/> entity.</param>
		/// <param name="encryptor">The instance of the <see cref="IEncryptor"/> to be used to encrypt the password of the user.</param>
		public UserService(ILogger<UserService> logger, IUserRepository userRespository, IEncryptor encryptor)
		{
			_logger = logger;
			_userRespository = userRespository;
			_encryptor = encryptor;
		}

		/// <summary>
		/// Adds a <see cref="User"/> to the database.
		/// </summary>
		/// <param name="user">Contains the information of the user to be added to the database.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the amount of entities affected by the operation.
		/// </returns>
		public Task<int> AddAsync(User user, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Encrypting the password");

			user.Password = _encryptor.Encrypt(user.Password);
			user.Created = DateTimeOffset.UtcNow;

			return _userRespository.AddAsync(user, cancellationToken);
		}

		/// <summary>
		/// Gets the chats the given user belongs to.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested chats.
		/// </returns>
		public Task<List<Chat>> GetChatsAsync(string userId, CancellationToken cancellationToken = default)
		{
			return _userRespository.GetChatsAsync(userId, cancellationToken);
		}

		/// <summary>
		/// Gets a single user by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested user.
		/// </returns>
		public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
		{
			return _userRespository.GetByIdAsync(id, cancellationToken);
		}
	}
}
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Abstracts the access to the database where the <see cref="User"/> will be saved and query.
	/// </summary>
	/// <remarks>This class implements the <see cref="IUserRepository"/> interface.</remarks>
	public class UserRepository : IUserRepository
	{
		/// <summary>
		/// Represents the connection to the database.
		/// </summary>
		private readonly ChatroomContext _context;

		/// <summary>
		/// Initializes a new instance of <see cref="UserRepository"/> type.
		/// </summary>
		/// <param name="context">The object used to access to the database.</param>
		public UserRepository(ChatroomContext context)
		{
			_context = context;
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
		public virtual Task<int> AddAsync(User user, CancellationToken cancellationToken = default)
		{
			_context.Users.Add(user);

			return _context.SaveChangesAsync(cancellationToken);
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
		public virtual Task<List<Chat>> GetChatsAsync(string userId, CancellationToken cancellationToken = default)
		{
			return _context.Users.AsNoTracking()
				.Include("Chats.Users")
				.Where(u => u.Id == userId)
				.SelectMany(u => u.Chats)
				.OrderByDescending(c => c.Created)
				.ToListAsync(cancellationToken);
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
		public virtual Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
		{
			return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
		}
	}
}

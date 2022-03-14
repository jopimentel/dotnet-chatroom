using Microsoft.EntityFrameworkCore;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class UserRepository : IUserRepository
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly ChatroomContext _context;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public UserRepository(ChatroomContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<int> AddAsync(User user, CancellationToken cancellationToken = default)
		{
			_context.Users.Add(user);

			return _context.SaveChangesAsync(cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<List<Chat>> GetChatsAsync(string userId, CancellationToken cancellationToken = default)
		{
			return _context.Users.AsNoTracking()
				.Include("Chats.Users")
				.Where(u => u.Id == userId)
				.SelectMany(u => u.Chats)
				.OrderByDescending(c => c.Created)
				.ToListAsync(cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
		{
			return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
		}
	}
}

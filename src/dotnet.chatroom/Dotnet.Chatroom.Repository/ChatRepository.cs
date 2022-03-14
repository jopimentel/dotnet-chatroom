namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class ChatRepository : IChatRepository
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly ChatroomContext _context;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public ChatRepository(ChatroomContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chat"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default)
		{
			_context.Chats.Add(chat);

			return _context.SaveChangesAsync(cancellationToken);
		}
	}
}

namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// 
	/// </summary>
	public interface IChatService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="chat"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task SaveMessageAsync<T>(Message<T> message, CancellationToken cancellationToken = default);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="audience"></param>
		/// <param name="itemsPerPage"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<List<Message<object>>> GetByAudienceAsync(string audience, int itemsPerPage = 50, CancellationToken cancellationToken = default);
	}
}

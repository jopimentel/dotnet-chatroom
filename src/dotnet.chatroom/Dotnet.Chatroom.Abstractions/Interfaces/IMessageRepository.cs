namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMessageRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task AddAsync<T>(Message<T> message, CancellationToken cancellationToken = default);

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

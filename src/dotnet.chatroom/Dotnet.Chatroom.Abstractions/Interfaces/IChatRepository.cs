namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public interface IChatRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="chat"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default);
	}
}

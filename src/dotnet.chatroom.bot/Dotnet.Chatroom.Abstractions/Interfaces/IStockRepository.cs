namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public interface IStockRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="stock"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task AddAsync(Stock stock, CancellationToken cancellationToken = default);
	}
}

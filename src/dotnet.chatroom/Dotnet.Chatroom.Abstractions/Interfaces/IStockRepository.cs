namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// Defines the abstraction to the database where the <see cref="Stock"/> will be saved and query.
	/// </summary>
	public interface IStockRepository
	{
		/// <summary>
		/// Adds a <see cref="Stock"/> to the database.
		/// </summary>
		/// <param name="stock">The <see cref="Stock"/> object to be added to the database.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		Task AddAsync(Stock stock, CancellationToken cancellationToken = default);
	}
}

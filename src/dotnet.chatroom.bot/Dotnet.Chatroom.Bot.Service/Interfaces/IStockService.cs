namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Defines a set of methods that allows to manage the <see cref="Stock"/> entity.
	/// </summary>
	/// <remarks>This definition is intended to be implementented in a business specific class.</remarks>
	public interface IStockService
	{
		/// <summary>
		/// Adds a <see cref="Stock"/> to the database.
		/// </summary>
		/// <remarks>The correlationId must be the one provided by the message requesting the creation of the stock.</remarks>
		/// <param name="correlationId">The primary key with which the entity will be created.</param>
		/// <param name="dataTransferObject">Contains the information obtained from the stooq api. This object is used to build the <see cref="Stock"/>.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the created stock.
		/// </returns>
		Task<Stock> AddAsync(string correlationId, StooqResponse dataTransferObject, CancellationToken cancellationToken = default);
	}
}

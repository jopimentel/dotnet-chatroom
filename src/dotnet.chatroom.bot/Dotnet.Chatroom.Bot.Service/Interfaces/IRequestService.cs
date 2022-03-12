namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Defines a set of methods that allows to manage the <see cref="Request"/> entity.
	/// </summary>
	/// <remarks>This definition is intended to be implementented in a business specific class.</remarks>
	public interface IRequestService
	{
		/// <summary>
		/// Adds a <see cref="Request"/> to the database and publish a message requesting a stock quote.
		/// </summary>
		/// <param name="dataTransferObject">Contains the information provided by the user. This object is used to build the <see cref="Request"/>.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the id of the request to be processed.
		/// </returns>
		Task<string> AddAsync(RequestStockQuote dataTransferObject, CancellationToken cancellationToken = default);
	}
}

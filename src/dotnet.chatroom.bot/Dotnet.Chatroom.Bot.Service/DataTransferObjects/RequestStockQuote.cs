namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Represents the request made by the user to get a certain stock quote.
	/// </summary>
	public class RequestStockQuote
	{
		/// <summary>
		/// The unique identifier of the stock quote.
		/// </summary>
		public string StockCode { get; set; }
		/// <summary>
		/// The operation to be executed.
		/// </summary>
		public string Action { get; set; }
	}
}

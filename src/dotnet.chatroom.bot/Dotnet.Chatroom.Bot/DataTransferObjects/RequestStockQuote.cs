namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	/// 
	/// </summary>
	public class RequestStockQuote
	{
		/// <summary>
		/// 
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StockCode { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Filename { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTimeOffset Date { get; set; }
	}
}

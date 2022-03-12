namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Represents the content of the csv file obtained from the stooq api.
	/// </summary>
	public class StooqResponse
	{
		/// <summary>
		/// The stock code.
		/// </summary>
		public string Symbol { get; set; }
		/// <summary>
		/// The date when the stock quote was requested.
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// The time when the stock quote was requested.
		/// </summary>
		public TimeSpan Time { get; set; }
		/// <summary>
		/// The stock quote value the day before the request.
		/// </summary>
		public decimal Open { get; set; }
		/// <summary>
		/// The highest stock quote value reached on the same day the request was made.
		/// </summary>
		public decimal High { get; set; }
		/// <summary>
		/// The lowest stock quote value reached on the same day the request was made.
		/// </summary>
		public decimal Low { get; set; }
		/// <summary>
		/// The stock quote value at the time the request was made.
		/// </summary>
		public decimal Close { get; set; }
		/// <summary>
		/// The number of shares traded.
		/// </summary>
		public long Volume { get; set; }
	}
}

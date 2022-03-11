namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	/// 
	/// </summary>
	public class StooqResponse
	{
		/// <summary>
		/// 
		/// </summary>
		public string Symbol { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TimeSpan Time { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal Open { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal High { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal Low { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal Close { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public long Volume { get; set; }
	}
}

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Stock
	{
		/// <summary>
		/// 
		/// </summary>
		public string Id { get; set; }
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
		/// <summary>
		/// 
		/// </summary>
		public Request Request { get; set; }
	}
}

namespace Dotnet.Chatroom
{
	/// <summary>
	/// The entity which defines the stock object.
	/// </summary>
	public class Stock : Entity
	{
		/// <summary>
		/// Refers to the stock code.
		/// </summary>
		public string Symbol { get; set; }
		/// <summary>
		/// Date the stock information was requested.
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Time the stock information was requested.
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
		/// <summary>
		/// The <see cref="Chatroom.Request"/> object with which the stock quote was requested. 
		/// </summary>
		public Request Request { get; set; }
	}
}

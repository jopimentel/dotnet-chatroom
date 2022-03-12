namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Request
	{
		/// <summary>
		/// 
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Command Command { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Audience { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTimeOffset Date { get; set; }
	}
}

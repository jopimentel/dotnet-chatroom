namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Message : Entity
	{
		/// <summary>
		/// 
		/// </summary>
		public string Emitter { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EmitterName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Audience { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MessageType Type { get; set; }
	}
}
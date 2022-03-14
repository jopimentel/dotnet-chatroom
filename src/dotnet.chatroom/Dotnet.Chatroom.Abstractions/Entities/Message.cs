namespace Dotnet.Chatroom
{
	/// <summary>
	/// The entity which defines the message object.
	/// </summary>
	public class Message : Entity
	{
		/// <summary>
		/// The unique identifier of who sent the message.
		/// </summary>
		public string Emitter { get; set; }
		/// <summary>
		/// The name of who sent the message.
		/// </summary>
		public string EmitterName { get; set; }
		/// <summary>
		/// Indicates whom the messages will be delivered.
		/// </summary>
		public string Audience { get; set; }
		/// <summary>
		/// Indicates the type of the message.
		/// </summary>
		public MessageType Type { get; set; }
	}
}
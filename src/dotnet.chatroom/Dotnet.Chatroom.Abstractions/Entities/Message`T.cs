namespace Dotnet.Chatroom
{
	/// <summary>
	/// Represents a generic message. Allows to specify the type of the content of the message.
	/// </summary>
	/// <typeparam name="T">Represents the type of the content of the message.</typeparam>
	public class Message<T> : Message
	{
		/// <summary>
		/// The inner content of the message.
		/// </summary>
		public T Content { get; set; }
	}
}
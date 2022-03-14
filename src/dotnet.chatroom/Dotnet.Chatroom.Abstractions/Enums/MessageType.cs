namespace Dotnet.Chatroom
{
	/// <summary>
	/// Defines the types of messages.
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// Represents the default messages. Default messages are considered all those are not a <see cref="Command"/>.
		/// </summary>
		Default,
		/// <summary>
		/// Refers to messages sent by the bot.
		/// </summary>
		Command
	}
}

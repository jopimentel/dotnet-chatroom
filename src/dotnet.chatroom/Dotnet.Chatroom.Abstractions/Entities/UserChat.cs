namespace Dotnet.Chatroom
{
	/// <summary>
	/// This entity indicates the relationship between an <see cref="Chatroom.User"/> and a <see cref="Chatroom.Chat"/>.
	/// </summary>
	public class UserChat : Entity
	{
		/// <summary>
		/// The primary key of the user.
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// The primary key of the chat.
		/// </summary>
		public string ChatId { get; set; }

		/// <summary>
		/// The user information that is related to the chat.
		/// </summary>
		public User User { get; set; }
		/// <summary>
		/// The chat information that the user belongs to.
		/// </summary>
		public Chat Chat { get; set; }
	}
}

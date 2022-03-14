namespace Dotnet.Chatroom
{
	/// <summary>
	/// The entity which defines the chat object.
	/// </summary>
	public class Chat : Entity
	{
		/// <summary>
		/// The name of the chat.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Refers to the type of the current chat.
		/// </summary>
		public ChatType Type { get; set; }

		/// <summary>
		/// Represents the set of users that belongs to the chat.
		/// </summary>
		public ICollection<User> Users { get; set;}
	}
}

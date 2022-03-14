namespace Dotnet.Chatroom
{
	/// <summary>
	/// The entity which defines the user object.
	/// </summary>
	public class User : Entity
	{
		/// <summary>
		/// The name of the user that will be used as a unique identifier.
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// The fullname of the user.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The email address of the user.
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// The super secret key of the user. This key allows the user to log in.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Represents the set of chats that the user belongs to.
		/// </summary>
		public ICollection<Chat> Chats { get; set; }
	}
}

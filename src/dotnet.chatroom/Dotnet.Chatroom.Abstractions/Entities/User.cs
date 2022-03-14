namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class User : Entity
	{
		/// <summary>
		/// 
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICollection<Chat> Chats { get; set; }
	}
}

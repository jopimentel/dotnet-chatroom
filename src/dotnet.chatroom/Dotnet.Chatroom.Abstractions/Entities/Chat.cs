namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Chat : Entity
	{
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ChatType Type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICollection<User> Users { get; set;}
	}
}

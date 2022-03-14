namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class UserChat : Entity
	{
		/// <summary>
		/// 
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ChatId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public User User { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Chat Chat { get; set; }
	}
}

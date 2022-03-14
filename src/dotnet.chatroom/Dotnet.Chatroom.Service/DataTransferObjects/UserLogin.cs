namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// Data transfer object used in the login operation.
	/// </summary>
	public class UserLogin
	{
		/// <summary>
		/// Refers to the username or email addres of the user.
		/// </summary>
		public string UsernameOrEmail { get; set; }
		/// <summary>
		/// The access key owner by the user.
		/// </summary>
		public string Password { get; set; }
	}
}

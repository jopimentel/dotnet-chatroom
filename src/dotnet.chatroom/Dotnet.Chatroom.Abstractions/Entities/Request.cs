namespace Dotnet.Chatroom
{
	/// <summary>
	/// The entity which defines the request object.
	/// </summary>
	public class Request : Entity
	{
		/// <summary>
		/// The information of the executed command.
		/// </summary>
		public Command Command { get; set; }
		/// <summary>
		/// Indicates whom the result of the executed command will be delivered to.
		/// </summary>
		public string Audience { get; set; }
	}
}

namespace Dotnet.Chatroom
{
	/// <summary>
	/// The entity which defines the command object.
	/// </summary>
	public class Command
	{
		/// <summary>
		/// Unique identifier of the command.
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// The concrete command.
		/// </summary>
		public string Action { get; set; }
		/// <summary>
		/// The value provided with the command.
		/// </summary>
		public string Value { get; set; }
	}
}

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Message<T> : Message
	{
		/// <summary>
		/// 
		/// </summary>
		public T Content { get; set; }
	}
}
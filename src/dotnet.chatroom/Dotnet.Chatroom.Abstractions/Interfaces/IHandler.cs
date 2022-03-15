namespace Dotnet.Chatroom
{
	/// <summary>
	/// A handler is the mechanism used to subscribe to a RabbitMQ queue.
	/// </summary>
	public interface IHandler
	{
		/// <summary>
		/// Refers to the queue the handler is watching.
		/// </summary>
		string Queue { get; }
	}
}

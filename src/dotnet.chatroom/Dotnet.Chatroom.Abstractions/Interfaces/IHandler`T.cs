using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Defines a generic handler. A handler is the mechanism used to subscribe to a RabbitMQ queue.
	/// </summary>
	/// <remarks>
	///		<para>The generic handler allows to convert a message to its corresponding type.</para>
	///		<para>This interfaces extends the <see cref="IHandler"/> interface.</para>
	///	</remarks>
	public interface IHandler<T> : IHandler
	{
		/// <summary>
		/// Handles the received message from RabbitMQ.
		/// </summary>
		/// <param name="data">The content of the message passed through RabbitMQ.</param>
		/// <param name="model"><see cref="IModel"/> object used to acknowledge the message.</param>
		/// <param name="arguments">Contains all the information about the delivered message.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		Task HandleAsync(T data, IModel model, BasicDeliverEventArgs arguments, CancellationToken cancellationToken = default);
	}
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public interface IHandler<T> : IHandler
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="model"></param>
		/// <param name="arguments"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task HandleAsync(T data, IModel model, BasicDeliverEventArgs arguments, CancellationToken cancellationToken = default);
	}
}

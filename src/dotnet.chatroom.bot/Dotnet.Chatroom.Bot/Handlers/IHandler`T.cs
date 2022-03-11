using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Chatroom.Bot
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
		/// <returns></returns>
		Task HandleAsync(T data, IModel model, BasicDeliverEventArgs arguments);
	}
}

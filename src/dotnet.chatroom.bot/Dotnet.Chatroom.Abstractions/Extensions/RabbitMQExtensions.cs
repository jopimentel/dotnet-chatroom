using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public static class RabbitMQExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <param name="data"></param>
		/// <param name="routingKey"></param>
		/// <param name="replayTo"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public static Task<string> PublishAsync<T>(this IModel model, T data, string routingKey, string replayTo = null, CancellationToken cancellationToken = default)
		{
			string correlationId = Guid.NewGuid().ToString();
			string body = JsonSerializer.Serialize(data);
			byte[] message = Encoding.UTF8.GetBytes(body);

			IBasicProperties properties = model.CreateBasicProperties();
			properties.CorrelationId = correlationId;

			if (!string.IsNullOrWhiteSpace(replayTo))
				properties.ReplyTo = replayTo;

			cancellationToken.ThrowIfCancellationRequested();
			model.BasicPublish(exchange: "", routingKey, mandatory: false, properties, message);

			return Task.FromResult(correlationId);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <param name="data"></param>
		/// <param name="routingKey"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public static Task<string> PublishAsync<T>(this IModel model, T data, string routingKey, CancellationToken cancellationToken = default)
		{
			return PublishAsync(model, data, routingKey, replayTo: null, cancellationToken);
		}
	}
}

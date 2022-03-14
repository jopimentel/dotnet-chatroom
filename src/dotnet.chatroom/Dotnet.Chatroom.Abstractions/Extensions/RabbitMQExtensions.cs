using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Extends the <see cref="IModel"/> type by adding additional functionalities.
	/// </summary>
	public static class RabbitMQExtensions
	{
		/// <summary>
		/// Publishes a message to the specified rounting key of RabbitMQ.
		/// </summary>
		/// <remarks>To keep things as simple as possible, exchanges are not in use.</remarks>
		/// <typeparam name="T">Represents the type of the object to be converted and sent to the queue.</typeparam>
		/// <param name="model">Allows to interact with the functionalities provided by RabbitMQ for the amqp protocol.</param>
		/// <param name="data">The message to be sent.</param>
		/// <param name="routingKey">The name of the queue to send the message to.</param>
		/// <param name="replyTo">The queue by which the response should be retrieved.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the correlation id of the published message.
		/// </returns>
		public static Task<string> PublishAsync<T>(this IModel model, T data, string routingKey, string replyTo = null, CancellationToken cancellationToken = default)
		{
			string correlationId = Guid.NewGuid().ToString();
			string body = JsonSerializer.Serialize(data);
			byte[] message = Encoding.UTF8.GetBytes(body);

			IBasicProperties properties = model.CreateBasicProperties();
			properties.CorrelationId = correlationId;

			if (!string.IsNullOrWhiteSpace(replyTo))
				properties.ReplyTo = replyTo;

			cancellationToken.ThrowIfCancellationRequested();
			model.BasicPublish(exchange: "", routingKey, mandatory: false, properties, message);

			return Task.FromResult(correlationId);
		}

		/// <summary>
		/// Publishes a message to the specified rounting key of RabbitMQ.
		/// </summary>
		/// <remarks>To keep things as simple as possible, exchanges are not in use.</remarks>
		/// <typeparam name="T">Represents the type of the object to be converted and sent to the queue.</typeparam>
		/// <param name="model">Allows to interact with the functionalities provided by RabbitMQ for the amqp protocol.</param>
		/// <param name="data">The message to be sent.</param>
		/// <param name="routingKey">The name of the queue to send the message to.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the correlation id of the published message.
		/// </returns>
		public static Task<string> PublishAsync<T>(this IModel model, T data, string routingKey, CancellationToken cancellationToken = default)
		{
			return PublishAsync(model, data, routingKey, replyTo: null, cancellationToken);
		}
	}
}

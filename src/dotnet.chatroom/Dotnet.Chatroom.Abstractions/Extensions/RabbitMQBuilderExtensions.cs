using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Extends the <see cref="RabbitMQBuilder"/> types by adding additional functionalities.
	/// </summary>
	public static class RabbitMQBuilderExtensions
	{
		/// <summary>
		/// Allows to declare the queues to be used by the application.
		/// </summary>
		/// <param name="builder"><see cref="RabbitMQBuilder"/> object used to register/configure services related to RabbitMQ.</param>
		/// <param name="queues">An array which contains the name of the queues to be declared.</param>
		/// <returns>The same instance of <see cref="RabbitMQBuilder"/> type used to register/configure the services.</returns>
		public static RabbitMQBuilder DeclareQueues(this RabbitMQBuilder builder, string[] queues)
		{
			builder.Services.Add(new ServiceDescriptor(typeof(IModel), provider =>
			{
				IConnection connection = provider.GetRequiredService<IConnection>();
				IModel model = connection.CreateModel();

				foreach (string queue in queues)
					model.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);

				return model;

			}, builder.Lifetime));

			return builder;
		}

		/// <summary>
		/// Dynamically register all of the <see cref="IHandler{T}"/> implementations from a given <see cref="Assembly"/>.
		/// </summary>
		/// <remarks>A handler is the mechanism used to subscribe to a RabbitMQ queue.</remarks>
		/// <param name="builder"><see cref="RabbitMQBuilder"/> object used to register/configure services related to RabbitMQ.</param>
		/// <param name="assembly">The <see cref="Assembly"/> where the <see cref="IHandler{T}"/> is implemented.</param>
		/// <returns>The same instance of <see cref="RabbitMQBuilder"/> type used to register/configure the services.</returns>
		public static RabbitMQBuilder AddHandlers(this RabbitMQBuilder builder, Assembly assembly)
		{
			IEnumerable<Type> types = GetHandlers(assembly);

			foreach (Type type in types)
				builder.Services.Add(new ServiceDescriptor(type, type, builder.Lifetime));

			ServiceProvider provider = builder.Services.BuildServiceProvider();
			IConnection connection = provider.GetRequiredService<IConnection>();

			foreach (Type type in types)
				AddHandler(type, provider, connection);

			return builder;
		}

		/// <summary>
		/// Register a single implementation of <see cref="IHandler{T}"/>.
		/// </summary>
		/// <remarks>By adding the <see cref="IHandler{T}"/> implementation, the subscription to the queue specified in <see cref="IHandler.Queue"/> is activated.</remarks>
		/// <param name="type">An implementation of <see cref="IHandler{T}"/>.</param>
		/// <param name="provider">The <see cref="ServiceProvider"/> used to get the required services.</param>
		/// <param name="connection">Represents the connection to RabbitMQ.</param>
		private static void AddHandler(Type type, ServiceProvider provider, IConnection connection)
		{
			Type genericType = GetGenericType(type);
			object instance = provider.GetService(type);
			IHandler handler = (IHandler)instance;
			IModel model = connection.CreateModel();

			model.QueueDeclare(handler.Queue, durable: true, exclusive: false, autoDelete: false);

			CancellationTokenSource tokenSource = new(Environment.HandleTimeout);
			EventingBasicConsumer consumer = new(model);

			consumer.Received += (object sender, BasicDeliverEventArgs arguments) =>
			{
				try
				{
					byte[] message = arguments.Body.ToArray();
					string body = Encoding.UTF8.GetString(message);
					object data = JsonSerializer.Deserialize(body, genericType);
					MethodInfo method = type.GetMethod("HandleAsync");

					method.Invoke(instance, parameters: new[] { data, model, arguments, tokenSource.Token });
				}
				catch (Exception) { }
			};

			model.BasicConsume(handler.Queue, autoAck: false, consumer);
		}

		/// <summary>
		/// Gets all types that implement the <see cref="IHandler{T}"/> interface on the given <see cref="Assembly"/>. 
		/// </summary>
		/// <remarks>The type must not be an <see langword="abstract"/> <see langword="class"/> in order to match the criteria used to find the implementations.</remarks>
		/// <param name="assembly">The <see cref="Assembly"/> where the implementations are located.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> which contains the types that implement the <see cref="IHandler{T}"/> interface.</returns>
		private static IEnumerable<Type> GetHandlers(Assembly assembly)
		{
			return assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IHandler).IsAssignableFrom(t));
		}

		/// <summary>
		/// Gets the type of the generic type argument used by an interface.
		/// </summary>
		/// <param name="type">The type which implements the interface.</param>
		/// <returns>The type of the generic type argument of the first interface with one argument.</returns>
		private static Type GetGenericType(Type type)
		{
			Type[] interfaces = type.GetInterfaces();
			Type @interface = interfaces.Where(i => i.GenericTypeArguments.Length == 1).FirstOrDefault();

			return @interface.GenericTypeArguments.FirstOrDefault();
		}
	}
}

using MongoDB.Driver;
using RabbitMQ.Client;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Extends the <see cref="IServiceCollection"/> types by adding additional functionalities.
	/// </summary>
	internal static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the <see cref="IMongoClient"/> to the <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> instance to be used to register the <see cref="IMongoClient"/>.</param>
		/// <param name="connectionString">Is the connection string to the desired database.</param>
		/// <param name="lifetime">Specifies the lifetime of the mongo client in the <see cref="IServiceCollection"/>.</param>
		/// <returns>The same instance of <see cref="IServiceCollection"/> type used to register the <see cref="IMongoClient"/>.</returns>
		public static IServiceCollection AddMongoClient(this IServiceCollection services, string connectionString, ServiceLifetime lifetime = ServiceLifetime.Singleton)
		{
			services.Add(new ServiceDescriptor(typeof(IMongoClient), _ => new MongoClient(connectionString), lifetime));

			return services;
		}

		/// <summary>
		/// Adds the <see cref="IMongoDatabase{T}"/> to the <see cref="IServiceCollection"/>.
		/// </summary>
		/// <remarks>The <see cref="IMongoDatabase{T}"/> allows to register multiple databases to the dependecies container.</remarks>
		/// <typeparam name="T">Represents the type used as the <see cref="IMongoDatabase{T}"/> generic type argument.</typeparam>
		/// <param name="services">The <see cref="IServiceCollection"/> instance to be used to register the <see cref="IMongoDatabase{T}"/>.</param>
		/// <param name="database">The name of the database to be attached to the instances of the service.</param>
		/// <param name="lifetime">Specifies the lifetime of the mongo database in the <see cref="IServiceCollection"/>.</param>
		/// <returns>The same instance of <see cref="IServiceCollection"/> type used to register the <see cref="IMongoDatabase{T}"/>.</returns>
		public static IServiceCollection AddMongoDatabase<T>(this IServiceCollection services, string database, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : class
		{
			MongoDatabaseOptions<T> options = new() { Database = database };

			services.Add(new ServiceDescriptor(typeof(MongoDatabaseOptions<T>), _ => options, lifetime));
			services.Add(new ServiceDescriptor(typeof(IMongoDatabase<T>), typeof(MongoDatabase<T>), lifetime));

			return services;
		}

		/// <summary>
		/// Adds the <see cref="IApplicationContext"/> to the <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> instance to be used to register the <see cref="IApplicationContext"/>.</param>
		/// <param name="lifetime">Specifies the lifetime of the application context in the <see cref="IServiceCollection"/>.</param>
		/// <returns>The same instance of <see cref="IServiceCollection"/> type used to register the <see cref="IApplicationContext"/>.</returns>
		public static IServiceCollection AddApplicationContext(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
		{
			services.AddHttpContextAccessor();
			services.Add(new ServiceDescriptor(typeof(IApplicationContext), typeof(ApplicationContext), lifetime));

			return services;
		}

		/// <summary>
		/// Allows to register several services related to RabbitMQ to the <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> instance to be used to register all the services.</param>
		/// <param name="uri">The uri used to establish the connection with RabbitMQ.</param>
		/// <param name="lifetime">Specifies the lifetime of the RabbitMQ services in the <see cref="IServiceCollection"/>.</param>
		/// <returns>An instance of <see cref="RabbitMQBuilder"/> type used to register/configure the services related to RabbitMQ.</returns>
		public static RabbitMQBuilder AddRabbitMQ(this IServiceCollection services, string uri, ServiceLifetime lifetime = ServiceLifetime.Scoped)
		{
			ConnectionFactory factory = new() { Uri = new Uri(uri) };

			services.Add(new ServiceDescriptor(typeof(IConnection), _ => factory.CreateConnection(), ServiceLifetime.Singleton));

			return new RabbitMQBuilder(services, lifetime);
		}
	}
}

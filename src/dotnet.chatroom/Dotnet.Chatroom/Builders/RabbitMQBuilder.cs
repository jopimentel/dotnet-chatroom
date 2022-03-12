namespace Dotnet.Chatroom
{
	/// <summary>
	/// Provides a simple API surface for configuring all RabbitMQ services.
	/// </summary>
	internal class RabbitMQBuilder
	{
		/// <summary>
		/// Refers to the <see cref="IServiceCollection"/> instance to be used to register all the services.
		/// </summary>
		public IServiceCollection Services { get; set; }
		/// <summary>
		/// Specifies the lifetime of the RabbitMQ services in the <see cref="IServiceCollection"/>.
		/// </summary>
		public ServiceLifetime Lifetime { get; set; }

		/// <summary>
		/// Initializes a new instance of <see cref="RabbitMQBuilder"/> type by providing the <see cref="IServiceCollection"/> instance to be used to register all the services.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> instance to be used to register all the services.</param>
		/// <param name="lifetime">The lifetime of the services in the <see cref="IServiceCollection"/>.</param>
		public RabbitMQBuilder(IServiceCollection services, ServiceLifetime lifetime)
		{
			Services = services;
			Lifetime = lifetime;
		}
	}
}

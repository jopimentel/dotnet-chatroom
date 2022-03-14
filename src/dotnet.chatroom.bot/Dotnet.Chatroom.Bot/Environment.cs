using Env = System.Environment;

namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	/// Provides a simple way to access to the environment variables defined for the application.
	/// </summary>
	internal static class Environment
	{
		/// <summary>
		/// The name of the current Application.
		/// </summary>
		public static string AppName => Env.GetEnvironmentVariable("APPLICATION_NAME");
		/// <summary>
		/// The origins to be used for the cors configuration.
		/// </summary>
		public static string[] Origins => Env.GetEnvironmentVariable("CORS_ORIGINS")?.Split(',', StringSplitOptions.RemoveEmptyEntries);
		/// <summary>
		/// The connection string for the MongoDB database.
		/// </summary>
		public static string MongoConnectionString => Env.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
		/// <summary>
		/// The name of the database where the documents will be stored.
		/// </summary>
		public static string MongoDatabase => Env.GetEnvironmentVariable("MONGO_DATABASE");
		/// <summary>
		/// The name of the database that will be used by the MongoDB GridFS.
		/// </summary>
		public static string MongoGridFSDatabase => Env.GetEnvironmentVariable("MONGO_GRID_FS_DATABASE");
		/// <summary>
		/// The connection string to the RabbitMQ server.
		/// </summary>
		public static string RabbitMQUri => Env.GetEnvironmentVariable("RABBITMQ_URI");
		/// <summary>
		/// The input queue used to publish the initial request to obtain the stock quote information.
		/// </summary>
		public static string StockQuoteIn => Env.GetEnvironmentVariable("QUEUE_STOCK_QUOTE_IN");
		/// <summary>
		/// The output queue used to publish the stock quote information obtained from the stooq api.
		/// </summary>
		public static string StockQuoteOut => Env.GetEnvironmentVariable("QUEUE_STOCK_QUOTE_OUT");
		/// <summary>
		/// The endpoint used to obtain the stock quote information.
		/// </summary>
		public static string StooqApi => Env.GetEnvironmentVariable("STOOQ_API");
	}
}

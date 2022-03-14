using Microsoft.AspNetCore.SignalR.Client;
using Env = System.Environment;

namespace Dotnet.Chatroom
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
		/// The connection string to the RabbitMQ server.
		/// </summary>
		public static string RabbitMQUri => Env.GetEnvironmentVariable("RABBITMQ_URI");
		/// <summary>
		/// The output queue used to publish the stock quote information obtained from the stooq api.
		/// </summary>
		public static string StockQuoteOut => Env.GetEnvironmentVariable("QUEUE_STOCK_QUOTE_OUT");
		/// <summary>
		/// The connection string for the mssql database.
		/// </summary>
		public static string MSSQLConnectionString => Env.GetEnvironmentVariable("MSSQL_CONNECTION_STRING");
		/// <summary>
		/// The url to the inner hub. This url allows to builds a <see cref="HubConnection"/> to invoke methods of <see cref="ChatsHub"/>.
		/// </summary>
		public static string MessageHub => Env.GetEnvironmentVariable("MESSAGES_HUB");
		/// <summary>
		/// The unique identifier of the bot. The bot is the one in charge of send the stock quote.
		/// </summary>
		public static string Bot => Env.GetEnvironmentVariable("BOT_IDENTIFIER");
		/// <summary>
		/// A friendly name for the <see cref="Bot"/>.
		/// </summary>
		public static string BotName => Env.GetEnvironmentVariable("BOT_NAME");
		/// <summary>
		/// A friendly description to be provided when the bot doesn't understand the command.
		/// </summary>
		public static string UnknownCommandMessage => Env.GetEnvironmentVariable("BOT_UNKNOWN_COMMAND_MESSAGE");
	}
}

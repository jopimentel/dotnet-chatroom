using Env = System.Environment;

namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Provides a simple way to access to the environment variables defined for the application.
	/// </summary>
	internal static class Environment
	{
		/// <summary>
		/// The input queue used to publish the initial request to obtain the stock quote information.
		/// </summary>
		public static string StockQuoteIn => Env.GetEnvironmentVariable("QUEUE_STOCK_QUOTE_IN");
		/// <summary>
		/// The output queue used to publish the stock quote information obtained from the stooq api.
		/// </summary>
		public static string StockQuoteOut => Env.GetEnvironmentVariable("QUEUE_STOCK_QUOTE_OUT");
	}
}

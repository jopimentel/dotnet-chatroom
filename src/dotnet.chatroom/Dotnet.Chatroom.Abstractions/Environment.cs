using Env = System.Environment;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Provides a simple way to access to the environment variables defined for the application.
	/// </summary>
	internal static class Environment
	{
		/// <summary>
		/// The time interval in miliseconds used to wait before canceling a operation.
		/// </summary>
		public static int HandleTimeout => int.Parse(Env.GetEnvironmentVariable("HANDLE_TIMOUT") ?? "30000");
	}
}

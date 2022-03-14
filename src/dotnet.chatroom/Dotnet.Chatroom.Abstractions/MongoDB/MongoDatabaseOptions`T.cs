using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// The options to be used to build a <see cref="MongoDatabase{T}"/> object.
	/// </summary>
	/// <typeparam name="T">Rerpresents the type used to distinguish the instance in the <see cref="IServiceCollection"/>.</typeparam>
	public class MongoDatabaseOptions<T>
	{
		/// <summary>
		/// The name of the database to which a instance of <see cref="MongoDatabase{T}"/> will use.
		/// </summary>
		public string Database { get; set; }
	}
}

using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Provides an abstraction for the <see cref="IMongoDatabase"/> interface.
	/// </summary>
	/// <typeparam name="T">Rerpresents the type used to distinguish the instances in the <see cref="IServiceCollection"/>.</typeparam>
	public class MongoDatabase<T> : IMongoDatabase<T>
	{
		/// <summary>
		/// Holds the object with the MongoDB database.
		/// </summary>
		public IMongoDatabase InnerDatabase { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="MongoDatabase{T}"/> type.
		/// </summary>
		/// <param name="client">The <see cref="IMongoClient"/> used to access to the database specified in the provided options.</param>
		/// <param name="options">he options to be used to build a <see cref="MongoDatabase{T}"/> object.</param>
		public MongoDatabase(IMongoClient client, MongoDatabaseOptions<T> options)
		{
			InnerDatabase = client.GetDatabase(options.Database);
		}

		/// <summary>
		/// Gets a collection by its name.
		/// </summary>
		/// <typeparam name="TCollection">Refers to the type of the discriminator to be used for the collection.</typeparam>
		/// <param name="name">The name of the collection.</param>
		/// <returns>An object which implements the <see cref="IMongoCollection{TDocument}"/> interface.</returns>
		public IMongoCollection<TCollection> GetCollection<TCollection>(string name)
		{
			return InnerDatabase.GetCollection<TCollection>(name);
		}
	}
}

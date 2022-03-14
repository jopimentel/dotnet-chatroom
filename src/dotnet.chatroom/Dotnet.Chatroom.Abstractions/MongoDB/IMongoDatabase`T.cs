using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Defines the abstraction for the <see cref="IMongoDatabase"/> interface.
	/// </summary>
	/// <typeparam name="T">Rerpresents the type used to distinguish the instances in the <see cref="IServiceCollection"/>.</typeparam>
	public interface IMongoDatabase<T>
	{
		/// <summary>
		/// Holds the object with the MongoDB database.
		/// </summary>
		IMongoDatabase InnerDatabase { get; }

		/// <summary>
		/// Gets a collection by its name.
		/// </summary>
		/// <typeparam name="TCollection">Refers to the type of the discriminator to be used for the collection.</typeparam>
		/// <param name="name">The name of the collection.</param>
		/// <returns>An object which implements the <see cref="IMongoCollection{TDocument}"/> interface.</returns>
		IMongoCollection<TCollection> GetCollection<TCollection>(string name);
	}
}

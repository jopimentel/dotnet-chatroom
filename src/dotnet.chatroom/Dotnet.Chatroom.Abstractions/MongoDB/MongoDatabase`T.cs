using MongoDB.Driver;

namespace Dotnet.Chatroom
{
	public class MongoDatabase<T> : IMongoDatabase<T>
	{
		public IMongoDatabase InnerDatabase { get; }

		public MongoDatabase(IMongoClient client, MongoDatabaseOptions<T> options)
		{
			InnerDatabase = client.GetDatabase(options.Database);
		}

		public IMongoCollection<TCollection> GetCollection<TCollection>(string name)
		{
			return InnerDatabase.GetCollection<TCollection>(name);
		}
	}
}

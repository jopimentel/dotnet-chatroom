using MongoDB.Driver;

namespace Dotnet.Chatroom
{
	public interface IMongoDatabase<T>
	{
		IMongoDatabase InnerDatabase { get; }

		IMongoCollection<TCollection> GetCollection<TCollection>(string name);
	}
}

using MongoDB.Bson;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public interface IFileRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<MemoryStream> GetStreamByNameAsync(string filename, CancellationToken cancellationToken = default);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newFilename"></param>
		/// <param name="filename"></param>
		/// <param name="stream"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<ObjectId> SaveToGridFSAsync(string newFilename, string filename, Stream stream, CancellationToken cancellationToken = default);
	}
}

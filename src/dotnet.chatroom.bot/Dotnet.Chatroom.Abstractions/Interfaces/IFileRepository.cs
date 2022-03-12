using MongoDB.Bson;
using MongoDB.Driver.GridFS;

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
		/// <param name="filename"></param>
		/// <param name="stream"></param>
		/// <param name="options"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<ObjectId> SaveToGridFSAsync(string filename, Stream stream, GridFSUploadOptions options, CancellationToken cancellationToken = default);
	}
}

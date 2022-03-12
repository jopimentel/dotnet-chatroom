using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class FileRepository : IFileRepository
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IMongoDatabase<FileRepository> _mongodb;
		/// <summary>
		/// 
		/// </summary>
		private readonly IGridFSBucket _bucket;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mongodb"></param>
		public FileRepository(IMongoDatabase<FileRepository> mongodb)
		{
			_mongodb = mongodb;
			_bucket = new GridFSBucket(_mongodb.InnerDatabase);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual Task<MemoryStream> GetStreamByNameAsync(string filename, CancellationToken cancellationToken = default)
		{
			MemoryStream stream = new();

			_bucket.DownloadToStreamByName(filename, stream, cancellationToken: cancellationToken);

			return Task.FromResult(stream);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="stream"></param>
		/// <param name="options"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual Task<ObjectId> SaveToGridFSAsync(string filename, Stream stream, GridFSUploadOptions options, CancellationToken cancellationToken = default)
		{
			return _bucket.UploadFromStreamAsync(filename, stream, options, cancellationToken);
		}
	}
}
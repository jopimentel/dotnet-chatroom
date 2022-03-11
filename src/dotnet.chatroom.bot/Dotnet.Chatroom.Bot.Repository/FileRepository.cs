using MongoDB.Bson;
using MongoDB.Driver;
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
		private readonly IMongoDatabase _mongodb;
		private readonly IGridFSBucket _bucket;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mongodb"></param>
		public FileRepository(IMongoDatabase mongodb)
		{
			_mongodb = mongodb;
			_bucket = new GridFSBucket(_mongodb);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public virtual Task<MemoryStream> GetStreamByNameAsync(string filename, CancellationToken cancellationToken = default)
		{
			MemoryStream stream = new();

			_bucket.DownloadToStreamByName(filename, stream, cancellationToken: cancellationToken);

			return Task.FromResult(stream);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newFilename"></param>
		/// <param name="filename"></param>
		/// <param name="stream"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual Task<ObjectId> SaveToGridFSAsync(string newFilename, string filename, Stream stream, CancellationToken cancellationToken = default)
		{
			GridFSUploadOptions options = new()
			{
				Metadata = new BsonDocument
				{
					{ "previous", filename }
				}
			};

			return _bucket.UploadFromStreamAsync(newFilename, stream, options, cancellationToken);
		}
	}
}
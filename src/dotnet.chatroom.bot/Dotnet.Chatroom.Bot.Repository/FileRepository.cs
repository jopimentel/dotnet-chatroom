using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// Abstracts the access to the database where the files to be saved and query with MongoDB GridFS are stored.
	/// </summary>
	/// <remarks>This class implements the <see cref="IFileRepository"/> interface.</remarks>
	public class FileRepository : IFileRepository
	{
		/// <summary>
		/// Represents the connection to the MongoDB database to be used to manage the files to be saved and query from MongoDB GridFS.
		/// </summary>
		private readonly IMongoDatabase<FileRepository> _mongodb;
		/// <summary>
		/// Allows to store and retrieve files from MongoDB.
		/// </summary>
		private readonly IGridFSBucket _bucket;

		/// <summary>
		/// Initializes a new instance of <see cref="FileRepository"/> type.
		/// </summary>
		/// <param name="mongodb">The connection to the MongoDB database to be used to manage the files.</param>
		public FileRepository(IMongoDatabase<FileRepository> mongodb)
		{
			_mongodb = mongodb;
			_bucket = new GridFSBucket(_mongodb.InnerDatabase);
		}

		/// <summary>
		/// Gets the <see cref="MemoryStream"/> object which represents the file saved with the specified name.
		/// </summary>
		/// <param name="filename">The name of the file to search for.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the file with the specified name.
		/// </returns>
		public virtual Task<MemoryStream> GetStreamByNameAsync(string filename, CancellationToken cancellationToken = default)
		{
			MemoryStream stream = new();

			_bucket.DownloadToStreamByName(filename, stream, cancellationToken: cancellationToken);

			return Task.FromResult(stream);
		}
		/// <summary>
		/// Adds to MongoDB GridFS the specified <see cref="Stream"/> object.
		/// </summary>
		/// <param name="filename">The current name of the file. The name of the file will be replaced but both names will be kept.</param>
		/// <param name="stream"><see cref="Stream"/> object which represents te file to be saved.</param>
		/// <param name="options">Rerpesents the options to be used for a upload operation.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the <see cref="ObjectId"/> provided by MongoDB GridFS.
		/// </returns>
		public virtual Task<ObjectId> SaveToGridFSAsync(string filename, Stream stream, GridFSUploadOptions options, CancellationToken cancellationToken = default)
		{
			return _bucket.UploadFromStreamAsync(filename, stream, options, cancellationToken);
		}
	}
}
using Dotnet.Chatroom.Bot.Repository;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Provides a set of methods that allows to manage the files to be saved and query from MongoDB GridFS.
	/// </summary>
	/// <remarks>This class implements the <see cref="IFileService"/> interface.</remarks>
	public class FileService : IFileService
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<FileService> _logger;
		/// <summary>
		/// Provides a set of methods that allows to manage the files to be saved and query from MongoDB GridFS. 
		/// </summary>
		private readonly IFileRepository _fileRepository;

		/// <summary>
		/// Initializes a new instance of <see cref="FileService"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="fileRepository">The <see cref="IFileRepository"/> used to manage the files.</param>
		public FileService(ILogger<FileService> logger, IFileRepository fileRepository)
		{
			_logger = logger;
			_fileRepository = fileRepository;
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
		public Task<MemoryStream> GetStreamByNameAsync(string filename, CancellationToken cancellationToken = default)
		{
			return _fileRepository.GetStreamByNameAsync(filename, cancellationToken);
		}

		/// <summary>
		/// Adds to MongoDB GridFS the specified <see cref="Stream"/> object.
		/// </summary>
		/// <param name="newFilename">The name under which the file will be saved.</param>
		/// <param name="filename">The current name of the file. The name of the file will be replaced but both names will be kept.</param>
		/// <param name="stream"><see cref="Stream"/> object which represents te file to be saved.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the <see cref="ObjectId"/> provided by MongoDB GridFS.
		/// </returns>
		public Task<ObjectId> SaveToGridFSAsync(string newFilename, string filename, Stream stream, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Setting the previous name of the file. Current filename is {filename} and new filename is {newFilename}", filename, newFilename);

			GridFSUploadOptions options = new()
			{
				Metadata = new BsonDocument
				{
					{ "previous", filename }
				}
			};

			_logger.LogInformation("Saving the file to the database");

			return _fileRepository.SaveToGridFSAsync(newFilename, stream, options, cancellationToken);
		}
	}
}
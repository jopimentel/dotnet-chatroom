﻿using Dotnet.Chatroom.Bot.Repository;
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
		/// Provides a set of methods that allows to manage the files to be saved and query from MongoDB GridFS. 
		/// </summary>
		private readonly IFileRepository _fileRepository;

		/// <summary>
		/// Initializes a new instance of <see cref="FileService"/> type.
		/// </summary>
		/// <param name="fileRepository">The <see cref="IFileRepository"/> used to manage the files.</param>
		public FileService(IFileRepository fileRepository)
		{
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
			GridFSUploadOptions options = new()
			{
				Metadata = new BsonDocument
				{
					{ "previous", filename }
				}
			};

			return _fileRepository.SaveToGridFSAsync(newFilename, stream, options, cancellationToken);
		}
	}
}
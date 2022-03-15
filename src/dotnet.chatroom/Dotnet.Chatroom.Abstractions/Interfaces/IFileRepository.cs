using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Dotnet.Chatroom.Bot.Repository
{
	/// <summary>
	/// Defines the abstraction to the database where the files to be saved and query with MongoDB GridFS are stored.
	/// </summary>
	public interface IFileRepository
	{
		/// <summary>
		/// Gets the <see cref="MemoryStream"/> object which represents the file saved with the specified name.
		/// </summary>
		/// <param name="filename">The name of the file to search for.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the file with the specified name.
		/// </returns>
		Task<MemoryStream> GetStreamByNameAsync(string filename, CancellationToken cancellationToken = default);

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
		Task<ObjectId> SaveToGridFSAsync(string filename, Stream stream, GridFSUploadOptions options, CancellationToken cancellationToken = default);
	}
}

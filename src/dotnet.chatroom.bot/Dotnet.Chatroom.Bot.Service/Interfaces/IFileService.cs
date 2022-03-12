using MongoDB.Bson;

namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// Defines a set of methods that allows to manage the files to be saved and query from MongoDB GridFS.
	/// </summary>
	/// <remarks>This definition is intended to be implementented in a business specific class.</remarks>
	public interface IFileService
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
		/// <param name="newFilename">The name under which the file will be saved.</param>
		/// <param name="filename">The current name of the file. The name of the file will be replaced but both names will be kept.</param>
		/// <param name="stream"><see cref="Stream"/> object which represents te file to be saved.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the <see cref="ObjectId"/> provided by MongoDB GridFS.
		/// </returns>
		Task<ObjectId> SaveToGridFSAsync(string newFilename, string filename, Stream stream, CancellationToken cancellationToken = default);
	}
}

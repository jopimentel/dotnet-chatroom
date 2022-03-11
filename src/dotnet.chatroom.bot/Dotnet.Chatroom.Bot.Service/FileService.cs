using Dotnet.Chatroom.Bot.Repository;

namespace Dotnet.Chatroom.Bot.Service
{
	/// <summary>
	/// 
	/// </summary>
	public class FileService
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IFileRepository _fileRepository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileRepository"></param>
		public FileService(IFileRepository fileRepository)
		{
			_fileRepository = fileRepository;
		}
	}
}
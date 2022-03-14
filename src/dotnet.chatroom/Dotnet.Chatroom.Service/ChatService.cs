using Dotnet.Chatroom.Repository;

namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// 
	/// </summary>
	public class ChatService : IChatService
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IChatRepository _chatRespository;
		private readonly IMessageRepository _messageRepository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chatRespository"></param>
		public ChatService(IChatRepository chatRespository, IMessageRepository messageRepository)
		{
			_chatRespository = chatRespository;
			_messageRepository = messageRepository;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chat"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default)
		{
			// TODO: Allow to attach users to the created chat
			chat.Created = DateTimeOffset.UtcNow;

			return _chatRespository.AddAsync(chat, cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task SaveMessageAsync<T>(Message<T> message, CancellationToken cancellationToken = default)
		{
			return _messageRepository.AddAsync(message, cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="audience"></param>
		/// <param name="itemsPerPage"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<List<Message<object>>> GetByAudienceAsync(string audience, int itemsPerPage = 50, CancellationToken cancellationToken = default)
		{
			return _messageRepository.GetByAudienceAsync(audience, itemsPerPage, cancellationToken);
		}
	}
}
using Dotnet.Chatroom.Repository;
using Microsoft.Extensions.Logging;

namespace Dotnet.Chatroom.Service
{
	/// <summary>
	/// Provides a set of methods that allows to manage the <see cref="Chat"/> entity.
	/// </summary>
	/// <remarks>This class implements the <see cref="IChatService"/> interface.</remarks>
	public class ChatService : IChatService
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<ChatService> _logger;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Chat"/> entity. 
		/// </summary>
		private readonly IChatRepository _chatRespository;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Message"/> entity. 
		/// </summary>
		private readonly IMessageRepository _messageRepository;
		/// <summary>
		/// Allows to encrypt and decrypt a text.
		/// </summary>
		private readonly IEncryptor _encryptor;

		/// <summary>
		/// Initializes a new instance of <see cref="UserService"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="chatRespository">The <see cref="IUserRepository"/> used to manage the <see cref="Chat"/> entity.</param>
		/// <param name="messageRepository">The <see cref="IUserRepository"/> used to manage the <see cref="Message"/> entity.</param>
		/// <param name="encryptor">The instance of the <see cref="IEncryptor"/> to be used to encrypt and the decrypt a <see langword="string"/> message.</param>
		public ChatService(ILogger<ChatService> logger, IChatRepository chatRespository, IMessageRepository messageRepository, IEncryptor encryptor)
		{
			_logger = logger;
			_chatRespository = chatRespository;
			_messageRepository = messageRepository;
			_encryptor = encryptor;
		}

		/// <summary>
		/// Creates a new chat.
		/// </summary>
		/// <remarks>If the <see cref="Chat.Users"/> collection is provided then, the users will be attached to the chat.</remarks>
		/// <param name="chat">The object with the information used to create the chat.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the amount of entities affected by the operation.
		/// </returns>
		public Task<int> AddAsync(Chat chat, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Adding the chat");

			// TODO: Allow to attach users to the created chat
			chat.Created = DateTimeOffset.UtcNow;

			return _chatRespository.AddAsync(chat, cancellationToken);
		}

		/// <summary>
		/// Allows to save to the database a message from a specific chat.
		/// </summary>
		/// <param name="message">The message to be added.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task{TResult}"/> that indicates the completation of the operation.</returns>
		public Task SaveMessageAsync<T>(Message<T> message, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Encrypting the message");

			message.Content = message.Encrypt(_encryptor);

			return _messageRepository.AddAsync(message, cancellationToken);
		}

		/// <summary>
		/// Gets the messages produced by the given audience.
		/// </summary>
		/// <remarks>By default the service just query the top 50.</remarks>
		/// <param name="audience">The unique identifier of the audience.</param>
		/// <param name="itemsPerPage">Amount of messages to be returned.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested messages produced by the given audience.
		/// </returns>
		public async Task<List<Message<object>>> GetByAudienceAsync(string audience, int itemsPerPage = 50, CancellationToken cancellationToken = default)
		{
			List<Message<object>> messages = await _messageRepository.GetByAudienceAsync(audience, itemsPerPage, cancellationToken);

			foreach (Message<object> message in messages)
				message.Content = message.Decrypt(_encryptor);

			return messages;
		}
	}
}
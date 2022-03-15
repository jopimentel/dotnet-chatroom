using Dotnet.Chatroom.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Enables the transmission of real-time messages.
	/// </summary>
	[EnableCors("cors")]
	public class ChatsHub : Hub
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<ChatsHub> _logger;
		/// <summary>
		/// Allows to save the messages to the database. 
		/// </summary>
		private readonly IChatService _chatService;
		/// <summary>
		/// Allows to encrypt and decrypt a text.
		/// </summary>
		private readonly IEncryptor _encryptor;

		/// <summary>
		/// Initializes a new instance of <see cref="ChatsHub"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="chatService">The <see cref="IChatService"/> used to manage the <see cref="Chat"/> and <see cref="Message"/> entities.</param>
		/// <param name="encryptor">The instance of the <see cref="IEncryptor"/> to be used to encrypt and the decrypt a <see langword="string"/> message.</param>
		public ChatsHub(ILogger<ChatsHub> logger, IChatService chatService, IEncryptor encryptor)
		{
			_logger = logger;
			_chatService = chatService;
			_encryptor = encryptor;
		}

		/// <summary>
		/// Sends it to all the subscribers.
		/// </summary>
		/// <param name="audience">The identifier used to subscribe to messages.</param>
		/// <param name="message">The message to be published.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		public async Task InvokeMessage(string audience, Message<string> message)
		{
			if (string.IsNullOrWhiteSpace(audience))
				return;

			message.Type = MessageType.Default;

			try
			{
				_logger.LogInformation(@"Invoking the method ""InvokeMessage"" for the audience: {audience}", audience);

				await SaveAndSendAsync(audience, message);
			}
			catch (Exception exception)
			{
				_logger.LogError(@"An exception occured while invoking the method ""InvokeMessage""");
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);
			}
		}

		/// <summary>
		/// Creates a messages from a <see cref="Stock"/> object and sends it to all the subscribers.
		/// </summary>
		/// <param name="stock">The <see cref="Stock"/> object which will be part of the message.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		public async Task InvokeStock(Stock stock)
		{
			string audience = stock?.Request?.Audience;

			if (stock == null || string.IsNullOrEmpty(audience))
				return;

			try
			{
				_logger.LogInformation(@"Invoking the method ""InvokeStock"" for the audience: {audience}", audience);

				if (string.IsNullOrEmpty(stock.Symbol))
				{
					_logger.LogInformation(@"The bot couldn't get/understand a given command.");
					await SaveAndSendAsync(audience, UnknownCommandMessage(audience));

					return;
				}

				Message<Stock> message = new()
				{
					Id = stock.Id,
					Type = MessageType.Command,
					Emitter = Environment.Bot,
					EmitterName = Environment.BotName,
					Audience = audience,
					Content = stock,
					Created = DateTimeOffset.UtcNow
				};

				await SaveAndSendAsync(audience, message);
			}
			catch (Exception exception)
			{
				_logger.LogError(@"An exception occured while invoking the method ""InvokeStock""");
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);
			}
		}

		/// <summary>
		/// Saves the message to database and publish it to all its subscribers.
		/// </summary>
		/// <param name="audience">The identifier used to subscribe to messages.</param>
		/// <param name="message">The message to be published.</param>
		/// <returns>A <see cref="Task"/> that indicates the completation of the operation.</returns>
		private async Task SaveAndSendAsync<T>(string audience, Message<T> message)
		{
			await _chatService.SaveMessageAsync(message);

			message.Content = message.Decrypt(_encryptor);

			await Clients.All.SendCoreAsync(audience, new[] { message });
		}

		/// <summary>
		/// Creates a message which indicates that the bot couldn't get/understand a given command.
		/// </summary>
		/// <returns>The built message.</returns>
		private static Message<string> UnknownCommandMessage(string audience)
		{
			return new Message<string>()
			{
				Id = Guid.NewGuid().ToString(),
				Type = MessageType.Default,
				Emitter = Environment.Bot,
				EmitterName = Environment.BotName,
				Audience = audience,
				Content = Environment.UnknownCommandMessage,
				Created = DateTimeOffset.UtcNow
			};
		}
	}
}

using Dotnet.Chatroom.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Chatroom.Controllers
{
	/// <summary>
	/// Exposes the <see cref="Chat"/> resource. Allows to manage all related to chats and messages.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("cors")]
	public class ChatsController : ControllerBase
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<ChatsController> _logger;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="Chat"/> and <see cref="Message{T}"/> entities. 
		/// </summary>
		private readonly IChatService _chatService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChatsController"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="chatService">The <see cref="IChatService"/> used to manage the <see cref="Chat"/> and <see cref="Message"/> entities.</param>
		public ChatsController(ILogger<ChatsController> logger, IChatService chatService)
		{
			_logger = logger;
			_chatService = chatService;
		}

		/// <summary>
		/// Creates a new chat.
		/// </summary>
		/// <param name="chat">The object with the information used to create the chat.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task{TResult}"/> that indicates the completation of the operation.</returns>
		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] Chat chat, CancellationToken cancellationToken = default)
		{
			if (chat == null)
				return BadRequest();

			_logger.LogInformation("Requesting the creation of {chat}", chat.Name);

			try
			{
				int result = await _chatService.AddAsync(chat, cancellationToken);

				if (result <= 0)
					return StatusCode(500, new HttpRequestException("Couldn't create the chat."));

				_logger.LogInformation("{chat} was succesfully created", chat.Name);

				return Ok();
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while requesting the creation of {chat}", chat.Name);
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);

				return StatusCode(500, exception.GetBaseException().Message);
			}
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
		[HttpGet("{audience:guid}")]
		public async Task<IActionResult> GetByAudienceAsync([FromRoute] string audience, [FromQuery] int itemsPerPage = 50, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(audience))
				return BadRequest(new ArgumentNullException(nameof(audience)));

			_logger.LogInformation("Getting {itemsPerPage} the messages for the audience: {audience}", audience);

			try
			{
				List<Message<object>> messages = await _chatService.GetByAudienceAsync(audience, itemsPerPage, cancellationToken);

				if (messages == null || messages.Count <= 0)
					return NoContent();

				_logger.LogInformation("{count} messages were succesfully obtained", messages.Count);

				return Ok(messages);
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while getting the messages of {audience}", audience);
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);

				return StatusCode(500, exception.GetBaseException().Message);
			}
		}
	}
}
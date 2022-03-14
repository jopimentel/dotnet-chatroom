using Dotnet.Chatroom.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Chatroom.Controllers
{
	/// <summary>
	/// Exposes the <see cref="User"/> resource. Allows to manage all related to chats and messages.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("cors")]
	public class UsersController : ControllerBase
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<ChatsController> _logger;
		/// <summary>
		/// Provides a set of methods that allows to manage the <see cref="User"/> entity. 
		/// </summary>
		private readonly IUserService _userService;

		/// <summary>
		/// Initializes a new instance of the <see cref="UsersController"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="userService">The <see cref="IUserService"/> used to manage the <see cref="User"/> entity.</param>
		public UsersController(ILogger<ChatsController> logger, IUserService userService)
		{
			_logger = logger;
			_userService = userService;
		}

		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="user">The object with the information used to create the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>A <see cref="Task{TResult}"/> that indicates the completation of the operation.</returns>
		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] User user, CancellationToken cancellationToken = default)
		{
			if (user == null)
				return BadRequest();

			_logger.LogInformation("Requesting the creation of {username}", user.Username);

			try
			{
				int result = await _userService.AddAsync(user, cancellationToken);

				if (result <= 0)
					return StatusCode(500, new HttpRequestException("Couldn't create the user."));

				_logger.LogInformation("{username} was succesfully created", user.Username);

				return Ok();
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while requesting the creation of {username}", user.Username);
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);

				return StatusCode(500, exception.GetBaseException().Message);
			}
		}

		/// <summary>
		/// Gets the chats the given user belongs to.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested chats.
		/// </returns>
		[HttpGet("{id:guid}/chats")]
		public async Task<IActionResult> GetChatsAsync([FromRoute] string id, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new ArgumentNullException(nameof(id)));

			_logger.LogInformation("Getting the chats of {id}", id);

			try
			{
				List<Chat> chats = await _userService.GetChatsAsync(id, cancellationToken);

				if (chats == null || chats.Count <= 0)
					return NoContent();

				// Changes the name of the chat for the user
				// who is not the one who made the request
				foreach (Chat chat in chats)
				{
					if (chat.Type != ChatType.Single)
						continue;

					User user = chat.Users.FirstOrDefault(u => u.Id != id);

					if (string.IsNullOrWhiteSpace(user.Name))
						continue;

					chat.Name = user.Name;
				}

				_logger.LogInformation("{count} chats were succesfully obtained", chats.Count);

				return Ok(chats);
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while getting the chats of {id}", id);
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);

				return StatusCode(500, exception.GetBaseException().Message);
			}
		}

		/// <summary>
		/// Gets a single user by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the requested user.
		/// </returns>
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new ArgumentNullException(nameof(id)));

			_logger.LogInformation("Getting user {id}", id);

			try
			{
				User user = await _userService.GetByIdAsync(id, cancellationToken);

				if (user == null)
					return NoContent();

				// Ensures password not to be part of the result
				user.Password = null;

				_logger.LogInformation("{id} was succesfully obtained", id);

				return Ok(user);
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while getting the user {id}", id);
				_logger.LogError("{message}", exception.GetBaseException().Message);
				_logger.LogError("{stackTrace}", exception.GetBaseException().StackTrace);

				return StatusCode(500, exception.GetBaseException().Message);
			}
		}
	}
}
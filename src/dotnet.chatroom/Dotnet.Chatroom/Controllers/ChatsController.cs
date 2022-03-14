using Dotnet.Chatroom.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Chatroom.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("cors")]
	public class ChatsController : ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IChatService _chatService;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chatService"></param>
		public ChatsController(IChatService chatService)
		{
			_chatService = chatService;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chat"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] Chat chat, CancellationToken cancellationToken = default)
		{
			try
			{
				int result = await _chatService.AddAsync(chat, cancellationToken);

				if (result > 0)
					return Ok();
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.GetBaseException().Message);
			}

			return StatusCode(500);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="audience"></param>
		/// <param name="itemsPerPage"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet("{audience:guid}")]
		public async Task<IActionResult> GetByAudienceAsync([FromRoute] string audience, [FromQuery] int itemsPerPage = 50, CancellationToken cancellationToken = default)
		{
			try
			{
				List<Message<object>> messages = await _chatService.GetByAudienceAsync(audience, itemsPerPage, cancellationToken);

				if (messages?.Count <= 0)
					return NoContent();

				return Ok(messages);
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.GetBaseException().Message);
			}
		}
	}
}
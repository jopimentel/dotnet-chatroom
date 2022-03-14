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
	public class UsersController : ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IUserService _userService;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userService"></param>
		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] User user, CancellationToken cancellationToken = default)
		{
			try
			{
				int result = await _userService.AddAsync(user, cancellationToken);

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
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet("{id:guid}/chats")]
		public async Task<IActionResult> GetChatsAsync([FromRoute] string id, CancellationToken cancellationToken = default)
		{
			try
			{
				List<Chat> chats = await _userService.GetChatsAsync(id, cancellationToken);

				if (chats.Count <= 0)
					return NoContent();


				foreach (Chat chat in chats)
				{
					if (chat.Type != ChatType.Single)
						continue;

					User audience = chat.Users.FirstOrDefault(u => u.Id != id);

					chat.Name = audience.Name;
				}

				return Ok(chats);
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.GetBaseException().Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken = default)
		{
			try
			{
				User user = await _userService.GetByIdAsync(id, cancellationToken);

				if (user == null)
					return NoContent();

				user.Password = null;

				return Ok(user);
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.GetBaseException().Message);
			}
		}
	}
}
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Chatroom.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			return await Task.FromResult(Ok("Juan Osiris Pimentel"));
		}
	}
}
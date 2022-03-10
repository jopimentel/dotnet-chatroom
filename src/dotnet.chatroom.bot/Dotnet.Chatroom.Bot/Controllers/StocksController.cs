using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Chatroom.Bot.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StocksController : ControllerBase
	{
		[HttpPost("{stockCode}")]
		public async Task<IActionResult> RequestQuoteByStockCodeAsync(string stockCode, CancellationToken cancellationToken = default)
		{
			return await Task.FromResult(Ok());
		}
	}
}
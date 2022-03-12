using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// Exposes the <see cref="Request"/> resource. Allows to manage all the requests made to get stock quotes.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("cors")]
	public class RequestsController : ControllerBase
	{
		/// <summary>
		/// Allows to log a message and use it to identify when a certain operation occurs.
		/// </summary>
		private readonly ILogger<RequestsController> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestsController"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="request">The <see cref="IRequestService"/> used to manage the <see cref="Request"/> entity.</param>
		public RequestsController(ILogger<RequestsController> logger)
		{
			_logger = logger;
		}
	}
}
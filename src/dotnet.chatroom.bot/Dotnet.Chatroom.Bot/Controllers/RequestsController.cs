using Microsoft.AspNetCore.Mvc;
using System.Net;
using Dotnet.Chatroom.Bot.Service;
using Microsoft.AspNetCore.Cors;

namespace Dotnet.Chatroom.Bot
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
		/// Provides a set of methods that allows to manage the <see cref="Request"/> entity. 
		/// </summary>
		private readonly IRequestService _request;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestsController"/> type.
		/// </summary>
		/// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> used to write log messages.</param>
		/// <param name="request">The <see cref="IRequestService"/> used to manage the <see cref="Request"/> entity.</param>
		public RequestsController(ILogger<RequestsController> logger, IRequestService request)
		{
			_logger = logger;
			_request = request;
		}

		/// <summary>
		/// Creates a request for a given stock.
		/// </summary>
		/// <param name="stockCode">The unique identifier of the stock quote.</param>
		/// <param name="request">The request made by the user to get the stock quote value.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> instance which indicates that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that indicates the completation of the operation.
		/// When the task completes, it contains the primary key of the created request.
		/// </returns>
		[HttpPost("{stockCode}")]
		public async Task<IActionResult> RequestQuoteByStockCodeAsync(string stockCode, [FromBody] RequestStockQuote request, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Requesting the stock quote of {stockCode}", stockCode);

			try
			{
				if (stockCode != request.StockCode)
					return BadRequest(new ArgumentException("The provided stock codes does not match", nameof(stockCode)));

				string correlationId = await _request.AddAsync(request, cancellationToken);

				_logger.LogInformation("The stock quote was succesfully requested and the commitment id is {correlationId}", correlationId);

				return Ok(new { correlationId });
			}
			catch (Exception exception)
			{
				_logger.LogError("An exception occured while requesting the stock quote of {stockCode}:", stockCode);
				_logger.LogError("{message}", exception.GetBaseException().Message);

				return StatusCode((int)HttpStatusCode.InternalServerError, exception);
			}
		}
	}
}
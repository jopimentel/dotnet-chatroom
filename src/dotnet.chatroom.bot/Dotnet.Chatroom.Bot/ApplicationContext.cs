namespace Dotnet.Chatroom.Bot
{
	/// <summary>
	/// Stores scoped data obtained from the current <see cref="HttpContent"/>.
	/// </summary>
	public class ApplicationContext : IApplicationContext
	{
		/// <summary>
		/// The unique identifier of the context.
		/// </summary>
		public string Id { get; } = Guid.NewGuid().ToString();
		/// <summary>
		/// Refers to those who are interested in the response of a certain request.
		/// </summary>
		public string Audience { get; }
		/// <summary>
		/// The exact date and time when the context was created.
		/// </summary>
		public DateTimeOffset Date { get; } = DateTimeOffset.UtcNow;

		/// <summary>
		/// Initializes a new instance of <see cref="ApplicationContext"/> type by specifying 
		/// the <see cref="IHttpContextAccessor"/> used to extract the desired data.
		/// </summary>
		/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/> object used to extract the desired data.</param>
		public ApplicationContext(IHttpContextAccessor httpContextAccessor)
		{
			IHeaderDictionary headers = httpContextAccessor?.HttpContext?.Request?.Headers;

			if (headers == null)
				return;

			Audience = headers["Audience"].ToString();
		}
	}
}

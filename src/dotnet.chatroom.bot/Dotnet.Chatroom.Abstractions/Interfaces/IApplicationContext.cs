namespace Dotnet.Chatroom
{
	/// <summary>
	/// Stores scoped data to decouple, as much as possible, application data and logic from technologies specific classes.
	/// </summary>
	public interface IApplicationContext
	{
		/// <summary>
		/// The unique identifier of the context.
		/// </summary>
		string Id { get; }
		/// <summary>
		/// Refers to those who are interested in the response of a certain request.
		/// </summary>
		string Audience { get; }
		/// <summary>
		/// The exact date and time when the context was created.
		/// </summary>
		DateTimeOffset Date { get; }
	}
}

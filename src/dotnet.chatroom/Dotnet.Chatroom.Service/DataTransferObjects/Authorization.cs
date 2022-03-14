namespace Dotnet.Chatroom
{
	/// <summary>
	/// Contains the information of the result of the authentication process.
	/// </summary>
	public struct Authorization
	{
		/// <summary>
		/// The user unique identifier.
		/// </summary>
		public string UserId { get; }
		/// <summary>
		/// Indicates whether or not a user is authorized to access to the application.
		/// </summary>
		public bool IsAuthorized { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="Authorization"/> type.
		/// </summary>
		/// <param name="userId">The user unique identifier.</param>
		/// <param name="isAuthorized">Indicates whether or not a user is authorized to access to the application.</param>
		public Authorization(string userId, bool isAuthorized)
		{
			UserId = userId;
			IsAuthorized = isAuthorized;
		}
	}
}

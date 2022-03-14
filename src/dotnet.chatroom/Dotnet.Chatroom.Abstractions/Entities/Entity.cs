namespace Dotnet.Chatroom
{
	/// <summary>
	/// Defines the base properties of each entity.
	/// </summary>
	public abstract class Entity
	{
		/// <summary>
		/// Unique identifier of the entity.
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// Creation date of the entity.
		/// </summary>
		public DateTimeOffset Created { get; set; }
	}
}

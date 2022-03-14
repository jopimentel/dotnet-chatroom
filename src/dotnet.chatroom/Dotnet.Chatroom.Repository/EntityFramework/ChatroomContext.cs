using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Represents the connection to the ChatRoom database.
	/// </summary>
	public class ChatroomContext : DbContext
	{
		/// <summary>
		/// Allows to save and query the <see cref="User"/> entity.
		/// </summary>
		public DbSet<User> Users { get; set; }
		/// <summary>
		/// Allows to save and query the <see cref="Chat"/> entity.
		/// </summary>
		public DbSet<Chat> Chats { get; set; }

		/// <summary>
		/// Initializes a new instance of <see cref="ChatroomContext"/> type by specifying the options to be used to build the object.
		/// </summary>
		/// <param name="options"></param>
		public ChatroomContext(DbContextOptions<ChatroomContext> options) : base(options) { }

		/// <summary>
		/// Allows to configures the entity to managed by the <see cref="ChatroomContext"/>.
		/// </summary>
		/// <param name="modelBuilder">The builder instance used to configure the entities.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Assembly assembly = Assembly.GetAssembly(typeof(ChatroomContext));

			if (assembly != null)
				modelBuilder.ApplyConfigurationsFromAssembly(assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// 
	/// </summary>
	public class ChatroomContext : DbContext
	{
		/// <summary>
		/// 
		/// </summary>
		public DbSet<User> Users { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DbSet<Chat> Chats { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		public ChatroomContext(DbContextOptions<ChatroomContext> options) : base(options) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Assembly assembly = Assembly.GetAssembly(typeof(ChatroomContext));

			if (assembly != null)
				modelBuilder.ApplyConfigurationsFromAssembly(assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}

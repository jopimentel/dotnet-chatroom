using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Allows to configure the entity <see cref="User"/> in a separate class.
	/// </summary>
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		/// <summary>
		/// Configures the entity of type <see cref="UserChat"/>.
		/// </summary>
		/// <param name="builder">The builder instance used to configure the entity.</param>
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User");
			builder.HasMany(u => u.Chats).WithMany(c => c.Users).UsingEntity<UserChat>
			(
				b => b.HasOne<Chat>().WithMany().HasForeignKey(c => c.ChatId),
				b => b.HasOne<User>().WithMany().HasForeignKey(u => u.UserId),
				b => b.HasKey(cu => new { cu.UserId, cu.ChatId })
			);
		}
	}
}

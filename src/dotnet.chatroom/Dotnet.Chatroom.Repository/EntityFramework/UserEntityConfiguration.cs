using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Chatroom.Repository
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
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

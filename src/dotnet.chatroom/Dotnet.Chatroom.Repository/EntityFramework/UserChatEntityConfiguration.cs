using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Chatroom.Repository
{
	public class UserChatEntityConfiguration : IEntityTypeConfiguration<UserChat>
	{
		public void Configure(EntityTypeBuilder<UserChat> builder)
		{
			builder.ToTable("UserChat");
		}
	}
}

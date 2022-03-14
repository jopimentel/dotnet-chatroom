using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Allows to configure the entity <see cref="UserChat"/> in a separate class.
	/// </summary>
	public class UserChatEntityConfiguration : IEntityTypeConfiguration<UserChat>
	{
		/// <summary>
		/// Configures the entity of type <see cref="UserChat"/>.
		/// </summary>
		/// <param name="builder">The builder instance used to configure the entity.</param>
		public void Configure(EntityTypeBuilder<UserChat> builder)
		{
			builder.ToTable("UserChat");
		}
	}
}

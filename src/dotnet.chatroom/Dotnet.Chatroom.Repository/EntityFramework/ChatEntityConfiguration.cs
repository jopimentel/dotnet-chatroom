using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Chatroom.Repository
{
	/// <summary>
	/// Allows to onfigure the entity <see cref="Chat"/> in a separate class.
	/// </summary>
	public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
	{
		/// <summary>
		/// Configures the entity of type <see cref="Chat"/>.
		/// </summary>
		/// <param name="builder">The builder instance used to configure the entity.</param>
		public void Configure(EntityTypeBuilder<Chat> builder)
		{
			builder.ToTable("Chat");
		}
	}
}

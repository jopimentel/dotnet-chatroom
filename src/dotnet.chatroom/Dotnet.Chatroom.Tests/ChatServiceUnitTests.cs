using Dotnet.Chatroom.Repository;
using Dotnet.Chatroom.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FizzWare.NBuilder;

namespace Dotnet.Chatroom.Tests
{
	public class ChatServiceUnitTests
	{
		[Fact]
		public async Task Should_Get_Chats_By_Audience()
		{
			// Arrange
			int itemsPerPage = 150;
			string audience = Guid.NewGuid().ToString();
			CancellationTokenSource tokenSource = new();
			IList<Message<object>> messagesMock = Builder<Message<object>>
				.CreateListOfSize(itemsPerPage).All()
				.With(m => m.Content = Guid.Empty.ToString())
				.Build();

			Mock<ILogger<ChatService>> loggerMock = new();
			Mock<IChatRepository> chatRepositoryMock = new();
			Mock<IMessageRepository> messageRepositoryMock = new();
			Mock<Encryptor> encryptorMock = new();

			messageRepositoryMock.Setup(m => m.GetByAudienceAsync(audience, itemsPerPage, tokenSource.Token)).ReturnsAsync(messagesMock as List<Message<object>>);
			encryptorMock.Setup(e => e.Decrypt(It.IsAny<string>()));

			// Act
			IChatService service = new ChatService(loggerMock.Object, chatRepositoryMock.Object, messageRepositoryMock.Object, encryptorMock.Object);
			List<Message<object>> messages = await service.GetByAudienceAsync(audience, itemsPerPage, tokenSource.Token);

			// Assert
			Assert.True(messages.Count > 0);
			messageRepositoryMock.Verify(c => c.GetByAudienceAsync(audience, itemsPerPage, tokenSource.Token), Times.Once);
			encryptorMock.Verify(e => e.Decrypt(It.IsAny<string>()), Times.Exactly(messages.Count));
		}

		[Theory]
		[InlineData("2c7a2a4f-7651-4026-9873-a21d26e95bcb", 150, 45)]
		[InlineData("91bdbd3a-3b72-44c8-8d77-56e1abd4b1ce", 25, 13)]
		[InlineData("51ebb7fb-5598-4920-9940-c9fa75457893", 100, 88)]
		public async Task Should_Get_Chats_By_Audience_And_Just_Decrypt_String_Messages(string audience, int itemsPerPage, int amountOfStringMessages)
		{
			// Arrange
			CancellationTokenSource tokenSource = new();
			IList<Message<object>> messagesMock = Builder<Message<object>>
				.CreateListOfSize(itemsPerPage)
				.TheFirst(amountOfStringMessages)
				.With(m => m.Content = Guid.Empty.ToString())
				.TheRest()
				.With(m => m.Content = new Stock())
				.Build();

			Mock<ILogger<ChatService>> loggerMock = new();
			Mock<IChatRepository> chatRepositoryMock = new();
			Mock<IMessageRepository> messageRepositoryMock = new();
			Mock<Encryptor> encryptorMock = new();

			messageRepositoryMock.Setup(m => m.GetByAudienceAsync(audience, itemsPerPage, tokenSource.Token)).ReturnsAsync(messagesMock as List<Message<object>>);
			encryptorMock.Setup(e => e.Decrypt(It.IsAny<string>()));

			// Act
			IChatService service = new ChatService(loggerMock.Object, chatRepositoryMock.Object, messageRepositoryMock.Object, encryptorMock.Object);
			List<Message<object>> messages = await service.GetByAudienceAsync(audience, itemsPerPage, tokenSource.Token);

			// Assert
			Assert.True(messages.Count > 0);
			messageRepositoryMock.Verify(c => c.GetByAudienceAsync(audience, itemsPerPage, tokenSource.Token), Times.Once);
			encryptorMock.Verify(e => e.Decrypt(It.IsAny<string>()), Times.Exactly(amountOfStringMessages));
		}
	}
}
using Dotnet.Chatroom.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	[EnableCors("cors")]
	public class ChatsHub : Hub
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IChatService _chatService;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chatService"></param>
		public ChatsHub(IChatService chatService)
		{
			_chatService = chatService;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="audience"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public async Task InvokeMessage(string audience, Message<string> message)
		{
			message.Type = MessageType.Default;

			await _chatService.SaveMessageAsync(message);
			await Clients.All.SendCoreAsync(audience, new[] { message });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stock"></param>
		/// <returns></returns>
		public async Task InvokeStock(Stock stock)
		{
			string audience = stock.Request.Audience;
			Message<Stock> message = new()
			{
				Id = stock.Id,
				Type = MessageType.Command,
				Emitter = Environment.Bot,
				EmitterName = "Bot",
				Audience = audience,
				Content = stock,
				Created = DateTimeOffset.UtcNow
			};

			await _chatService.SaveMessageAsync(message);
			await Clients.All.SendCoreAsync(audience, new[] { message });
		}
	}
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Dotnet.Chatroom
{
	[EnableCors("cors")]
	public class MessagesHub : Hub
	{
		public async Task InvokeMessage(string message)
		{
			await Clients.All.SendCoreAsync("messages-1", new[] { message });
		}

		public async Task InvokeStock(Stock stock)
		{
			await Clients.All.SendCoreAsync("messages-1", new[] { stock });
		}
	}
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Dotnet.Chatroom
{
	[EnableCors("cors")]
	public class MessagesHub : Hub
	{

	}
}

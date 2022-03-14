export const environment = {
  production: true,
  commands: [
    '/stock='
  ],
  bot: '00000000-0000-0000-0000-000000000000',
  endpoints: {
    chat: 'http://dotnet-chatroom/api/chats',
    chats: 'http://dotnet-chatroom/api/users/{id}/chats',
    hubs: 'http://dotnet-chatroom/hub',
    requests: 'http://dotnet-chatroom-bot/api/requests/{stockCode}',
    user: 'http://dotnet-chatroom/api/users/{id}'
  }
};
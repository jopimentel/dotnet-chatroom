export const environment = {
  production: false,
  commands: [
    '/stock='
  ],
  bot: '00000000-0000-0000-0000-000000000000',
  endpoints: {
    chat: 'http://localhost:25594/api/chats',
    chats: 'http://localhost:25594/api/users/{id}/chats',
    hubs: 'http://localhost:25594/hub',
    requests: 'http://localhost:8082/api/requests/{stockCode}',
    user: 'http://localhost:25594/api/users/{id}',
    messages: 'http://localhost:25594/api/chats/{audience}?itemsPerPage=50'
  }
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatsModule } from './chats/chats.module';
import { HttpClientModule } from '@angular/common/http';
import { UsersInChatModule } from './users-in-chat/users-in-chat.module';
import { MessagesModule } from './messages/messages.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports:[
    ChatsModule,
    UsersInChatModule,
    MessagesModule
  ]
})
export class Modules { }

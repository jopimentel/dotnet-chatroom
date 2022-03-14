import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatsModule } from './chats/chats.module';
import { HttpClientModule } from '@angular/common/http';
import { UsersInChatModule } from './users-in-chat/users-in-chat.module';
import { MessagesModule } from './messages/messages.module';
import { HomeModule } from './home/home.module';
import { LoginModule } from './login/login.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule
  ],
  exports:[
    ChatsModule,
    UsersInChatModule,
    MessagesModule,
    HomeModule,
    LoginModule
  ]
})
export class Modules { }

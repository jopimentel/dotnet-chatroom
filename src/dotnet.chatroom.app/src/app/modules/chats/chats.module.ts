import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatsComponent } from './chats.component';
import { ChatListComponent } from './chat-list/chat-list.component';

@NgModule({
  declarations: [
    ChatsComponent,
    ChatListComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ChatsComponent,
    ChatListComponent
  ]
})
export class ChatsModule { }

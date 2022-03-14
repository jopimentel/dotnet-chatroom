import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersInChatComponent } from './users-in-chat.component';



@NgModule({
  declarations: [
    UsersInChatComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    UsersInChatComponent
  ]
})
export class UsersInChatModule { }

import { Component, Input, ViewEncapsulation } from '@angular/core';
import { IChat } from '../entities/chat.model';
import { IUser } from '../entities/user.mode';
import { ChatType } from '../enums/chat-type.enum';

@Component({
  selector: 'app-users-in-chat',
  templateUrl: './users-in-chat.component.html',
  styleUrls: ['./users-in-chat.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UsersInChatComponent {

  @Input()
  public chat?: IChat;
  public chatType = ChatType;

  public get users(): Array<IUser> {
    return this.chat?.users ?? [];
  }
}

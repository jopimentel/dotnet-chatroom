import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { IChat } from '../../entities/chat.model';
import { ChatType } from '../../enums/chat-type.enum';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html'
})
export class ChatListComponent {

  @Input()
  public chats: Array<IChat> = [];
  public chatType = ChatType;
  @Output('switch')
  public chatChange: EventEmitter<IChat>;

  constructor() {
    this.chatChange = new EventEmitter<IChat>();
  }

  public switchTo(chat: IChat): void {
    this.chatChange.emit(chat);
  }
}

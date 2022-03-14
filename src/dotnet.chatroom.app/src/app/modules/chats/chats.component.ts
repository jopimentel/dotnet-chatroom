import { AfterViewInit, Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';
import { IChat } from '../entities/chat.model';
import { ChatService } from '../services/chat.service';
import { v4 as guid } from 'uuid';
import { ChatType } from '../enums/chat-type.enum';
import { tap } from 'rxjs';
import { UserService } from '../services/user.service';
import { IUser } from '../entities/user.mode';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ChatsComponent implements AfterViewInit {

  @Input()
  public user?: IUser;
  public chats: Array<IChat> = [];
  @Output('chats')
  public chatsChange: EventEmitter<Array<IChat>>;
  @Output('switch')
  public chatChange: EventEmitter<IChat>;

  constructor(
    private readonly service: ChatService,
    private readonly userService: UserService
  ) {
    this.chatsChange = new EventEmitter<Array<IChat>>();
    this.chatChange = new EventEmitter<IChat>();
  }

  public ngAfterViewInit(): void {
    this.getChats();
  }

  public add(name: string): void {
    const chat: IChat = {
      id: guid(),
      name,
      type: ChatType.chatroom,
      created: new Date()
    }

    this.service.add(chat)
      .pipe(
        tap(() => console.log(`The chat ${name} was added`))
      ).subscribe()
  }

  public getChats(): void {
    if (!this.user)
      return;

    this.userService.getChats(this.user.id)
      .pipe(
        tap((chats) => this.chats = chats),
        tap((chats) => this.chatsChange.emit(chats))
      ).subscribe()
  }

  public switchTo(chat: IChat): void {
    this.chatChange.emit(chat);
  }

}

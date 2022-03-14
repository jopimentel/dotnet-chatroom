import { Component, AfterViewInit, Output, EventEmitter, Input, OnDestroy, ViewEncapsulation, ViewChild, ElementRef } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { from, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IStockQuoteRequest } from '../../stock-quote-request';
import { IAlert } from '../entities/alert.model';
import { IChat } from '../entities/chat.model';
import { IMessage } from '../entities/message.model';
// import { AlertType } from '../enums/alert-type.enum';
import { BotService } from '../services/bot.service';
import { HubService } from '../services/hub.service';
import { v4 as guid } from 'uuid';
import { MessageType } from '../enums/messate-type.enum';
import { IUser } from '../entities/user.mode';
import { ChatService } from '../services/chat.service';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class MessagesComponent implements AfterViewInit, OnDestroy {

  @Input()
  public user?: IUser;
  @Input()
  public chat?: IChat;
  public connection: HubConnection;
  public messages: Array<any> = [];
  @Output('alert')
  public alertChange: EventEmitter<IAlert>;
  public messageType = MessageType;
  public environment = environment;
  @ViewChild('text', { static: true })
  public text?: ElementRef;

  constructor(
    private readonly hub: HubService,
    private readonly bot: BotService,
    private readonly chatService: ChatService
  ) {
    this.connection = this.hub.buildHubConnection('chats');
    this.alertChange = new EventEmitter<IAlert>();
  }

  public ngAfterViewInit(): void {
    this.getByAudience();

    from(this.connection.start())
      .pipe(
        tap(() => this.subscribeToMessages())
      ).subscribe();
  }

  public ngOnDestroy(): void {
    this.connection.stop();
  }

  public send(value: string): void {
    if (!this.chat?.id || !this.user)
      return;

    if (this.text)
      this.text.nativeElement.value = '';

    const command = environment.commands.find(c => this.isCommand(value, c));

    if (command) {
      this.request(this.chat?.id, value, command)
      return;
    }

    const { id: emitter, username: emitterName } = this.user;
    const message: IMessage<string> = {
      id: guid(),
      type: MessageType.default,
      emitter,
      emitterName,
      audience: this.chat?.id,
      content: value,
      created: new Date()
    };

    this.alertChange.emit({});
    this.connection.invoke('InvokeMessage', this.chat?.id, message);
  }

  public request(audience: string, message: string, command: string): void {
    this.alertChange.emit({});

    const body: IStockQuoteRequest = {
      stockCode: message.replace(command, ''),
      action: command
    };

    this.bot.requestQuote(body, audience)
      .pipe(
        tap(r => console.log(r))
      ).subscribe();
  }

  public reset(): void {
    this.messages = [];
    this.connection.stop();
    this.connection = this.hub.buildHubConnection('chats');
    this.getByAudience();

    from(this.connection.start())
      .pipe(
        tap(() => this.subscribeToMessages())
      ).subscribe();
  }

  public getCommandContent(content: any): string {
    const stockCode: string = content.symbol ?? content.Symbol;
    const close: string = content.close ?? content.Close;
    const price = new DecimalPipe('en-US').transform(close, '0.2-2');

    return `${stockCode.toUpperCase()} quote is $${price} per share`;
  }

  private getByAudience(): void {
    if (this.chat)
      this.chatService.getByAudience(this.chat?.id)
        .pipe(
          tap(messages => this.messages = messages),
          tap(messages => console.log(messages))
        ).subscribe();
  }

  private isCommand(value: string, command: string): boolean {
    return new RegExp(`^${command}`).test(value);
  }

  private subscribeToMessages(): void {
    if (!this.chat?.id)
      return;

    this.connection.on(this.chat.id, (message: any) => {
      this.messages.unshift(message);
    });
  }
}

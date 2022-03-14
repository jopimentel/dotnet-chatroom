import { Component, OnDestroy, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Subscription, switchMap, tap } from 'rxjs';
import { IAlert } from './modules/entities/alert.model';
import { IChat } from './modules/entities/chat.model';
import { IUser } from './modules/entities/user.mode';
import { AlertType } from './modules/enums/alert-type.enum';
import { MessagesComponent } from './modules/messages/messages.component';
import { UserService } from './modules/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnDestroy {

  @ViewChild('messages', { static: false })
  public messages?: MessagesComponent;
  public user?: IUser;
  public currentChat?: IChat;
  public alert: IAlert = {};
  private subscription: Subscription;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly userService: UserService
  ) {
    this.subscription = this.route.queryParams
      .pipe(
        map(query => query['user']),
        filter(user => user),
        switchMap((user: string) => this.userService.getById(user)),
        tap(user => this.user = user)
      ).subscribe();
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public showAlert(alert: IAlert): void {
    this.alert = alert;
  }

  public alertType(alert: IAlert): string {
    return alert.type == AlertType.error ? 'error' : 'info';
  }

  public switchChat(chat: IChat): void {
    this.currentChat = chat;
    this.messages?.reset();
  }
}
import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Subscription, switchMap, tap } from 'rxjs';
import { IAlert } from '../entities/alert.model';
import { IChat } from '../entities/chat.model';
import { IUser } from '../entities/user.mode';
import { AlertType } from '../enums/alert-type.enum';
import { MessagesComponent } from '../messages/messages.component';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements AfterViewInit, OnDestroy {

  @ViewChild('messages', { static: false })
  public messages?: MessagesComponent;
  public user?: IUser;
  public currentChat?: IChat;
  public alert: IAlert = {};
  private subscription?: Subscription;
  public loading: boolean = true;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly userService: UserService
  ) {

  }

  public ngAfterViewInit(): void {
    this.subscription = this.route.params
      .pipe(
        map(query => query['user']),
        switchMap((user: string) => this.userService.getById(user)),
        tap(user => {
          this.user = user;
          this.loading = false;
        })
      ).subscribe();
  }

  public ngOnDestroy(): void {
    this.subscription?.unsubscribe();
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

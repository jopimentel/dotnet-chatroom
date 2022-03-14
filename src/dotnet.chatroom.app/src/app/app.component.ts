import { Component, OnDestroy, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Subscription, switchMap, tap } from 'rxjs';
import { IUser } from './modules/entities/user.mode';
import { UserService } from './modules/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnDestroy {

  public user?: IUser;
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

}
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { filter, tap } from 'rxjs';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent {

  constructor(
    private readonly userService: UserService,
    private readonly router: Router
  ) { }

  public access(user: string, password: string): void {
    this.userService.login(user, btoa(password))
      .pipe(
        tap(auth => console.log(auth)),
        filter(auth => auth.isAuthorized),
        tap(auth => this.naviagteToHome(auth.userId)),
      ).subscribe();
  }

  private naviagteToHome(user: string): void {
    this.router.navigateByUrl(`home/${user}`);
  }

}

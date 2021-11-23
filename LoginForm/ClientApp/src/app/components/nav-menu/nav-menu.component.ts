import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private authService: AuthService, private jwtHelper: JwtHelperService) { }


  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  isUsrAuth(): boolean {
    return this.authService.isUserAuthenticated();
  }

  logOut() {
    return this.authService.logOut();
  }
}

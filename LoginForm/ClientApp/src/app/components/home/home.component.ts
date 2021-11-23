import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  constructor(private authService: AuthService) { }

  userAuthorized() {
    return this.authService.isUserAuthenticated();
  }

}

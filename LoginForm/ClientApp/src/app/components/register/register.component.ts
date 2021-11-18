import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { Account } from '../../models/account.model';

import { ApiService } from '../../services/api/api.service';
import { RegisterService } from '../../services/register/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  account: Account[];
  user = new Account();

  constructor(private http: HttpClient, private api: ApiService, private registerService: RegisterService) { }

  addAccount() {
    this.registerService.registerNewAccount(this.user)
      .subscribe(data => {
        console.log(data)})
  }
}

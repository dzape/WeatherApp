import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  constructor(private http: HttpClient) {}

  baseUrl = 'https://localhost:44316/api/Accounts/'

  registerUser(acc: Account) {
    
  }
}

export interface Account {
  username: string;
  password: string;
}

import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:44316/api/Accounts/'

  
  registerUser(a: string, b: string) {
    this.http.post(this.baseUrl, { username: a, password: b }).subscribe(res => {
      console.log(res);
      if (res == null) {
        alert("User alredy exist");
      }
    });
  }
}

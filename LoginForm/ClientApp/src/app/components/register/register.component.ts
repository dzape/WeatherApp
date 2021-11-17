import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  constructor(private http: HttpClient, private api: ApiService) { }
  
  registerUser(a: string, b: string) {
    this.http.post(this.api.getApiUrl(), { username: a, password: b }).subscribe(res => {
      console.log(res);
      if (res == null) {
        alert("User alredy exist");
      }
    });
  }
}

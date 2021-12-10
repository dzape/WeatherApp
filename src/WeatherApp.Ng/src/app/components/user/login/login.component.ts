import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../../services/api/cityapi/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {

  invalidLogin: boolean = false;
  constructor(private router: Router, private http: HttpClient, private apiService: ApiService) { }

  login(form: NgForm) {
    const credentials = {
      'username': form.value.username,
      'password': form.value.password,
    }
    const headers = { 'content-type': 'application/json' }

    this.http.post(this.apiService.getApiUrl() + 'auth/login', credentials,  { 'headers': headers })
      .subscribe(responce => {
        const token = (<any>responce).token;
        const username = credentials.username;
        localStorage.setItem("jwt", token);
        localStorage.setItem("username", username);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
      }, err => {
        this.invalidLogin = true;
      });
  }
}

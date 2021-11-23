import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {

  invalidLogin: boolean;
  constructor(private router: Router, private http: HttpClient, private apiService: ApiService) { }

  login(form: NgForm) {
    const credentials = {
      'username': form.value.username,
      'password': form.value.password,
    }

    this.http.post(this.apiService.getApiUrl() + 'auth/login', credentials)
      .subscribe(responce => {
        const token = (<any>responce).token;
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
      }, err => {
        this.invalidLogin = true;
      });
  }
}

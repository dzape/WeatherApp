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

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      observe: 'response' as 'response'
    };  

    this.http.post(this.apiService.getApiUrl() + 'auth/login', credentials,  httpOptions)
      .subscribe(response => {
        console.log(response);
        const token = (<any>response).body.token;
        const username = credentials.username;
        sessionStorage.setItem("jwt", token);
        sessionStorage.setItem("username", username);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
      } ,err => {
        this.invalidLogin = true;
      });
  }
}

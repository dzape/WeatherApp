import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account } from 'src/app/data/models/account.model';

import { ApiService } from '../api/cityapi/api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,private api: ApiService) { }
  id: any;

  token = localStorage.getItem('token');
  header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token);
  
  username: any = localStorage.getItem("username");
  updateUser(acc: Account){
      this.http.put(this.api.getApiUrl() + "users/" , { headers : this.header }).subscribe(Response => {
        if (Response === null) {
          console.log(" UPDATE FAILED ");
        }
      })
    }
  
  deleteUser(username: string){
    this.http.delete(this.api.getApiUrl());
  }
}

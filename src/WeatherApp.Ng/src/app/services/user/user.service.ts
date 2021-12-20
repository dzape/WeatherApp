import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account } from 'src/app/data/models/user/account.model';
import { UpdateAccModel } from 'src/app/data/models/user/updateacc.model';

import { ApiService } from '../api/cityapi/api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,private api: ApiService) { }

  token = sessionStorage.getItem('token');
  header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token);
  
  username: any = sessionStorage.getItem("username");
  updateUser(acc: UpdateAccModel): Observable<any>{
    acc.oldUsername = this.username;
    return this.http.put(this.api.getApiUrl() + "users/" , acc ,  { headers : this.header })
  }
  
  registerNewAccount(acc: Account): Observable<any> {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(acc);
    console.log(body)
    return this.http.post(this.api.getApiUrl() + 'auth/register', body, { 'headers': headers ,observe: 'response'} )
  }

  deleteUser(username: string){
    this.http.delete(this.api.getApiUrl());
  }
}

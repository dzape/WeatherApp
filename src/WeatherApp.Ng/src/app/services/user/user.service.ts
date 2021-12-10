import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Account } from 'src/app/data/models/account.model';

import { ApiService } from '../api/cityapi/api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,private api: ApiService) { }
  id: any;

  getUserIdByName(username: string) {
    return this.http.get(this.api.getApiUrl() + `users/getid/?username=${username}`);
  }
   
  getUsersByName(usr?: string): void {
    this.http.get(this.api.getApiUrl() + usr).subscribe(Response => {
      if (Response === null) {
        console.log("Error");
      }
    })
  }

  username: any = localStorage.getItem("username");
  updateUser(acc: Account){
    this.getUserIdByName(this.username).subscribe((myId) => {
      const body = {
        'id': myId,
        'username': acc.username,
        'password': acc.password
      }
      this.http.put(this.api.getApiUrl() + "users/" + myId, body ).subscribe(Response => {
        if (Response === null) {
          console.log(" UPDATE FAILED ");
        }
      })
    }
  )}

  deleteUser(username: string){
    this.http.delete(this.api.getApiUrl() + this.getUserIdByName(username));
  }
}

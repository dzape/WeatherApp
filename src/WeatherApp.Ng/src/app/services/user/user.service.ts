import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

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
}

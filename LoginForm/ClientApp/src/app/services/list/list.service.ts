import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Data } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  constructor(private http: HttpClient) { }

  baseurl: string = 'https://localhost:44316/api/Accounts'

  getUserById(id: number) {
    return this.http.get<Data>(this.baseurl)
  }
}

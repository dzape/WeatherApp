import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Account } from '../../../data/models//account.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  account!: Account;
  apiUrl: string = 'https://localhost:44322/api/'

  constructor(private http: HttpClient) { }

  getApiUrl() {
    return this.apiUrl;
  }

  li: any;
  getUsers() {   
    return this.http.get(this.getApiUrl())
  }
}

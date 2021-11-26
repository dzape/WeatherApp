import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Account } from '../../models/account.model';
import { IAccount } from '../../models/iaccount.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  account: Account;
  apiUrl: string = 'https://localhost:5001/api/' //44316

  constructor(private http: HttpClient) { }

  getApiUrl() {
    return this.apiUrl;
  }

  li: any;
  getUsers() {   
    return this.http.get(this.getApiUrl())
  }
}

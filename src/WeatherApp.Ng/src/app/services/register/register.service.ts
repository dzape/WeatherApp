import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account } from '../../data/models/account.model';

import { ApiService } from '../api/cityapi/api.service'

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient,private api: ApiService) { }

  registerNewAccount(acc: Account): Observable<any> {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(acc);
    console.log(body)
    return this.http.post(this.api.getApiUrl() + 'users', body, { 'headers': headers })
  }
}

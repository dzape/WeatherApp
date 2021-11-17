import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Data } from '@angular/router';

import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  constructor(private http: HttpClient,private api: ApiService) { }

  getUserById(id: number) {
    return this.http.get<Data>(this.api.getApiUrl());
  }
}

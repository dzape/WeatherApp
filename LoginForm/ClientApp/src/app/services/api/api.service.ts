import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl: string = 'https://localhost:44316/api/accounts'

  constructor() { }

  getApiUrl() {
    return this.apiUrl;
  }
}

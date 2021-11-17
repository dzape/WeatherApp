import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl: string = 'https://localhost:5001/api/accounts'

  constructor() { }

  getApiUrl() {
    return this.apiUrl;
  }
}

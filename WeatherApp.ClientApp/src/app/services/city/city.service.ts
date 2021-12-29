import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ThrowStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { City } from '../../data/models/city.model';

import { OpenweatherapiService } from '../api/openweatherapi/openweatherapi.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  private weatherApiUrl = 'https://localhost:44322/api/city/';
  constructor(private http: HttpClient,  private owapi: OpenweatherapiService) { }

  /* SessionStorage  */
  token = sessionStorage.getItem('jwt');
  header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token );
  username = sessionStorage.getItem('username');
  headers = { "Authorization": 'Bearer ' + this.token }
  
  getFavouriteCities(): Observable<any>{
    return this.http.get(this.weatherApiUrl,{ headers: this.header });
  }

  postFavCity(cityData: City) {
    return this.http.post(this.weatherApiUrl, cityData, { headers: this.headers });
  }
  
  deleteFavCity(city: City) {
    const body = JSON.stringify(city);

    let options = {
      headers: new HttpHeaders({
        'content-type': 'application/json' ,
        "Authorization": 'Bearer ' + this.token
      }),
      body: body
    }
    return this.http.delete('https://localhost:44322/api/city/', options);
  }
}

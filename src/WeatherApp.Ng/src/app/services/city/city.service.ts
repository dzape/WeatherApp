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

  citydata: any;
  cities: any = {};

  private weatherApiUrl = 'https://localhost:44322/api/city/';

  constructor(private http: HttpClient,  private owapi: OpenweatherapiService) { }

  token = localStorage.getItem('token');
  header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token);

  getFavCityes(accountId: any){
    return this.http.get(this.weatherApiUrl + accountId , { headers : this.header });
  }

  data: any;
  id!: number;
  citylist: any;

  getCityByName(name: string){
    return this.http.get(this.weatherApiUrl + name , { headers: this.header });
  }

  getFavouriteCities(username: string): Observable<any>{
    return this.http.get(this.weatherApiUrl + username, { headers: this.header});
  }

  postFavCity(cityData: any) {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(cityData);
    console.log(body, "Here ");
    return this.http.post(this.weatherApiUrl, body, { 'headers': headers });
  }

  deleteFavCity(id: number) {
    return this.http.delete<City>(this.weatherApiUrl + id);
  }
}

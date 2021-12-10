import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

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

  getFavCityes(accountId: any){
    return this.http.get(this.weatherApiUrl + accountId);
  }

  data: any;
  id!: number;
  citylist: any;

  getObseravbeCity() {
    if (this.cities.length > 0) {
      for (var i = 0; i <= this.cities.length - 1; i++) {
        this.owapi.getWeather(this.cities[i].cityName).subscribe((citydata) => {
          this.citydata[i] = citydata
          console.log(this.citydata);
        });
      }
      return this.citydata;
    }
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

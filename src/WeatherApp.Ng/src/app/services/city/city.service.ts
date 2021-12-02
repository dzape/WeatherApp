import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { iWeather } from 'src/app/data/models/iweather';

import { City } from '../../data/models/city.model';
import { UserService } from '../../services/user/user.service';
import { OpenweatherapiService } from '../api/openweatherapi/openweatherapi.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CityService {

  weatherData: any = {};
  citydata: any;
  cities: any = {};

  private weatherApiUrl = 'https://localhost:44322/api/city/';

  constructor(private http: HttpClient, private userService: UserService, private owapi: OpenweatherapiService) { }

  //return cityes for user if any
  getFavCityes(accountId: any){
    return this.http.get(this.weatherApiUrl + accountId);
  }

  data: any;
  id!: number;
  citylist: any;

  getObseravbeCity() {
    // this.userService.getUserIdByName(localStorage.getItem("username"))).subscribe((id) => {
    //   this.getFavCityes(Number(id)).subscribe((cities) => (this.cities = cities));
    //   console.log(this.cities);
    // });
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

  //add city to favourite
  postFavCity(cityData: any) {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(cityData);
    console.log(body, "Here ");
    return this.http.post(this.weatherApiUrl, body, { 'headers': headers });
  }

  // remove favourite city
  deleteFavCity(city: City) {
    const url = `${this.weatherApiUrl}${city.id}`;
    return this.http.delete<City>(url);
  }
}

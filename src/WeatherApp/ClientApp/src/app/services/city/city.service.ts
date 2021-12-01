import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { iWeather } from '../../models/iweather';
import { City } from '../../models/city.model';
import { UserService } from '../../services/user/user.service'
import { OpenWeatherApiService } from '../openweather-api/openweather-api.service'
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
  citydata;
  cities: any = {};

  private weatherApiUrl = 'https://localhost:44316/api/Weather/';

  constructor(private http: HttpClient, private userService: UserService, private owapi: OpenWeatherApiService) { }

  //return cityes for user if any
  getFavCityes(accountId: any){
    return this.http.get(this.weatherApiUrl + accountId);
  }

  data: any;
  id: number;
  citylist;

  getObseravbeCity(): Observable<any> {
    this.userService.getUserIdByName(localStorage.getItem("username").toString()).subscribe((id) => {
      this.getFavCityes(Number(id)).subscribe((cities) => (this.cities = cities));
      console.log(this.cities);
    });
    if (this.cities.length > 0) {
      for (var i = 0; i <= this.cities.length - 1; i++) {
        this.owapi.getWeather(this.cities[i].cityName).subscribe((citydata) => {
          this.citydata[i] = citydata
          console.log(this.citydata[i]);

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
    const url = `${this.weatherApiUrl}/${city.cityId}`;
    return this.http.delete<City>(url);
  }
}

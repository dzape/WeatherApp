import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { iWeather } from '../../models/iweather';
import { Weather } from '../../models/weather.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  weather: any = {};

  private weatherApiUrl = 'https://localhost:44316/api/weather/';

  constructor(private http: HttpClient) { }

  //return cityes for user if any
  getFavCityes(accountId: any){
    return this.http.get(this.weatherApiUrl + accountId);
  }

  //add city to favourite
  postFavCity(city: iWeather) {
    return this.http.post<iWeather>(this.weatherApiUrl, city, httpOptions)
  }

  // remove favourite city
  deleteFavCity(city: iWeather) {
    const url = `${this.weatherApiUrl}/${city.id}`;
    return this.http.delete<iWeather>(url);
  }
}

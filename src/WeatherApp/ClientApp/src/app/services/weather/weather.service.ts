import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { iWeather } from '../../models/iweather';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  private weatherApiUrl = 'https://localhost:44316/api/weather/';


  constructor(private http: HttpClient) { }


  //return cityes for user if any
  getFavCityes(accountId: number): Observable<iWeather[]> {
    return this.http.get<iWeather[]>(this.weatherApiUrl + accountId);
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
}s

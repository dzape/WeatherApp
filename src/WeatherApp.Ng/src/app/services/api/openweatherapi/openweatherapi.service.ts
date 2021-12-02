import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, EMPTY, Observable } from 'rxjs';

type NewType = Observable<any>;

@Injectable({
  providedIn: 'root'
})
export class OpenweatherapiService {

  private baseWeatherURL = 'https://api.openweathermap.org/data/2.5/weather?q=';
  private urlSuffix = "&units=metric&APPID=a50e2aff21f6864e4b65258a3b3ea856";

  constructor(private http: HttpClient) { }

  getWeather(city: string): Observable<any> {
    return this.http.get(this.baseWeatherURL + city + this.urlSuffix);
  }
}

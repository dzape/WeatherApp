import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WeatherApiService {

  private baseWeatherURL = 'https://api.openweathermap.org/data/2.5/weather?q=';
  private urlSuffix = "&units=metric&APPID="; // Input your api Key

  constructor(private http: HttpClient) { }

  getWeather(city: string): Observable<any> {
    return this.http.get(this.baseWeatherURL + city + this.urlSuffix)
      .pipe(catchError(err => {
        if (err.status === 404) {
          console.log(`City ${city} not found`);
          return EMPTY
        }
      })
    );
  }
}

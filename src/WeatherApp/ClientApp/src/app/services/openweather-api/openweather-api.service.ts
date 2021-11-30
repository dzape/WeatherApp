import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OpenWeatherApiService {

  private baseWeatherURL = 'https://api.openweathermap.org/data/2.5/weather?q=';
  private urlSuffix = "&units=metric&APPID=a50e2aff21f6864e4b65258a3b3ea856"; // Input your api Key

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
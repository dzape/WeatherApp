import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, of, pipe } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenweatherapiService {

  private baseWeatherURL = "https://api.openweathermap.org/data/2.5/weather?q=";
  private urlSuffix = "&units=metric&APPID=a50e2aff21f6864e4b65258a3b3ea856";

  constructor(private http: HttpClient) { }

  getWeatherCity(city: string, contry?: string): Observable<any> {
    return this.http.get(this.baseWeatherURL + city + this.urlSuffix)
    .pipe(
      catchError(error => {
        console.log('City not found');
        return of(0);     
      })
    )
  }

  getWeatherCityAndCountry(city: string, contry: string): Observable<any> {
    return this.http.get(this.baseWeatherURL + city + ',' + contry + this.urlSuffix)
    .pipe(
      catchError(error => {
        console.log('City not found');
        return of(0);     
      })
    )
  }
}

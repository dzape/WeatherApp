import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable, EMPTY } from 'rxjs';
import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { catchError, debounceTime, switchMap } from 'rxjs/operators';
import { WeatherApiService } from '../../services/weather-api/weather-api.service'
import { Weather } from '../../models/Weather.model';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',

})
export class WeatherComponent implements OnInit {

  @Output() onAddCity: EventEmitter<Weather> = new EventEmitter();

  searchInput = new FormControl();
  weather: any = {};

  constructor(private http: HttpClient, private weatherApiService: WeatherApiService) { }

  ngOnInit() {

    this.searchInput.valueChanges
      .pipe(debounceTime(200),
        switchMap(city => this.weatherApiService.getWeather(city)))
      .subscribe(
        res => {
          this.weather['city'] = res['main'].city;
          this.weather['temp'] = res['main'].temp + ' ℃';
          this.weather['humidity'] = res['main'].humidity + ' %';
          this.weather['description'] = res['weather'][0].description;
          this.weather['wind'] = res['wind'].speed + ' km/h';

          console.log(res);
        },
        err => console.log(`Can't get weather. Error code: %s, URL: %s`,
          err.message, err.url)
      );
  }

  addNewCity() {

    const newCity: any = {
      cityName: this.weather['city'],
      temp: this.weather['temp'],
      humidity: this.weather['humidity'],
      desciption: this.weather.description,
      wind: this.weather['wind']
    }

    this.onAddCity.emit(newCity);

    this.weather['city'] = '';
    this.weather['temp'] = '';
    this.weather['humidity'] = '';
    this.weather['description'] = '';
    this.weather['wind'] = '';
  }

}

//pag

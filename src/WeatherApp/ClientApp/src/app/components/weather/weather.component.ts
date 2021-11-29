import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { catchError, debounceTime, switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';

import { WeatherApiService } from '../../services/weather-api/weather-api.service'
import { WeatherService } from '../../services/weather/weather.service'
import { UserService } from '../../services/list/user.service';
import { Local } from 'protractor/built/driverProviders';
import { Weather } from '../../models/weather.model';
import { iWeather } from '../../models/iweather';
import { NgLocaleLocalization } from '@angular/common';
import { Account } from '../../models/account.model';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
})

export class WeatherComponent implements OnInit {

  userData: Account[];

  searchInput = new FormControl();
  weather: any = {};
  cityList: any = {};
  joTest: any;
  constructor(private http: HttpClient, private userService: UserService, private weatherApiService: WeatherApiService, private weatherService: WeatherService) { }

  ngOnInit() {

    this.getFavouriteCity();

    
    this.searchInput.valueChanges
      .pipe(debounceTime(200),
        switchMap(city => this.weatherApiService.getWeather(city)))
      .subscribe(
        res => {
          this.weather['city'] = res['main'].city;
          this.weather['temp'] = res['main'].temp + ' ℃';
          this.weather['humidity'] = res['main'].humidity + ' %';
          this.weather['description'] = res['weather'][0].description;
          this.weather['wind'] = res['wind'].speed + ' km/h'
        },

        err => console.log(`Can't get weather. Error code: %s, URL: %s`,
          err.message, err.url)
      );
  }

  getFavouriteCity() {
    this.userService.getUserIdByName(localStorage.getItem("username")).subscribe((response) => {
      this.weatherService.getFavCityes(response).subscribe((res) => {
        console.log(res);
      });
    });
  }
    
  addFavouriteCity(city: any = {}) {
    if (this.weather.content != "") {
      // POST request to db
    }
  }
}

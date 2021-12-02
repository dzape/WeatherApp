import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { debounceTime, switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';

import { OpenweatherapiService } from '../../services/api/openweatherapi/openweatherapi.service'
import { CityService } from '../../services/city/city.service'
import { UserService } from '../../services/user/user.service';
import { iWeather } from '../../data/models/iweather';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
})

export class WeatherComponent implements OnInit {

  public weatherData: iWeather[] = [];

  searchInput = new FormControl();
  weather: any = {};
  cities: any = {};
  username: any = localStorage.getItem("username");

  constructor(private http: HttpClient, private userService: UserService, private weatherApiService: OpenweatherapiService, private cityService: CityService) { }

  ngOnInit() {
    this.searchInput.valueChanges
      .pipe(debounceTime(200),
        switchMap(city => {
            return this.weatherApiService.getWeather(city);
        }))
      .subscribe(
        res => {
          this.weather['name'] = res.name;
          this.weather['temp'] = res['main'].temp + ' ℃';
          this.weather['humidity'] = res['main'].humidity + ' %';
          this.weather['description'] = res['weather'][0].description;
          this.weather['wind'] = res['wind'].speed + ' km/h'
        },
        err => console.log(`Can't get weather. Error code: %s, URL: %s`,
          err.message, err.url)
    );
  }

  addFavouriteCity() {
    this.userService.getUserIdByName(this.username).subscribe(id => {
      const credentials = {
        'cityName': this.weather['name'],
        'accountId': Number(id)
      }
      this.cityService.postFavCity(credentials).subscribe(data => {
        if (data === null) {
          alert("Search for a city");
        }
      });
      console.log(" City added to favourite cities :) ");
    })
  }
}

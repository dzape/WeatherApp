import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { OpenweatherapiService } from '../../services/api/openweatherapi/openweatherapi.service'
import { CityService } from '../../services/city/city.service'
import { UserService } from '../../services/user/user.service';
import { iWeather } from '../../data/models/iweather';
import { City } from 'src/app/data/models/city.model';
import { debounceTime, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

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
      .pipe(debounceTime(300),
        switchMap(city =>  this.weatherApiService.getWeather(city)))
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
      )
  }

  addFavouriteCity() {
    this.userService.getUserIdByName(this.username.toString()).subscribe(id => {
      const credentials = {
        'name': this.weather['name'],
        'userid': Number(id)
      }
      this.cityService.postFavCity(credentials).subscribe(data => {
        if (credentials.name === null) {
          alert("Search for a city");
        }
      });
      console.log(" City added to favourite cities :) ");
    })
  }
}

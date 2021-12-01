import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { catchError, debounceTime, switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';

import { OpenWeatherApiService } from '../../services/openweather-api/openweather-api.service'
import { CityService } from '../../services/city/city.service'
import { UserService } from '../../services/user/user.service';
import { Local } from 'protractor/built/driverProviders';
import { City } from '../../models/city.model';
import { iWeather } from '../../models/iweather';
import { NgLocaleLocalization } from '@angular/common';
import { Account } from '../../models/account.model';
import { debounce } from 'rxjs-compat/operator/debounce';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
})

export class CityComponent implements OnInit {

  public weatherData: iWeather[] = [];
  cities: any = {};

  constructor(private http: HttpClient, private userService: UserService, private weatherApiService: OpenWeatherApiService, private cityService: CityService) { }

  ngOnInit() {
    this.getFavouriteCity();
  }

  getFavouriteCity() {
    this.userService.getUserIdByName(localStorage.getItem("username")).subscribe((response) => {
      this.cityService.getFavCityes(response).subscribe((res) => {
        this.cities = res;
        if (this.cities.length > 0) {
          for (var i = 0; i <= this.cities.length - 1; i++) {
            this.weatherApiService.getWeather(this.cities[i].cityName).subscribe((cityWeatherData) => {
              this.weatherData.push(cityWeatherData);
              console.log("City Weather Data : ", this.weatherData);
            })
          }
        }
      });
    });
  }

  onDelete(city: City) {
    for (var i = 0; i < this.cities.length; i++) {
      if (city.name.toString() === this.cities[i].cityName.toString()) {
        console.log(city.name);
        city.id = Number(this.cities[i].cityId);
        this.cityService.deleteFavCity(city).subscribe((cities) => this.cities = this.cities.filter( c => c.id !== this.cities.id ));
        console.log("You have deleted : ", city.name);
      }
    }
  }

}

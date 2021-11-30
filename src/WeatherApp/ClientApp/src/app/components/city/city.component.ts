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

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
})

export class CityComponent implements OnInit {

  public weatherData: iWeather[] = [];

  searchInput = new FormControl();
  citiydata;
  weather: any = {};
  cities: any = {};

  constructor(private http: HttpClient, private userService: UserService, private weatherApiService: OpenWeatherApiService, private cityService: CityService) { }

  ngOnInit() {
    this.cityService.getObseravbeCity().subscribe((cities) => this.cities = cities);
  }

    
  //addFavouriteCity() {
  //  this.userService.getUserIdByName(localStorage.getItem("username")).subscribe(id => {
  //    const credentials = {
  //      'cityName': this.weather['name'],
  //      'accountId': Number(id)
  //    }
  //    this.cityService.postFavCity(credentials).subscribe(data => {
  //      if (data === null) {
  //        alert("Search for a city");
  //      }
  //      console.log("ERROR");
  //    });
  //    console.log("YEE");
  //  })
  //}

  //deleteFavouriteCity() {

  //}
}

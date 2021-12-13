import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

import { iWeather } from '../../data/models/iweather';

import { AuthService } from '../../services/auth/auth.service';
import { CityService } from '../../services/city/city.service'
import { UserService } from '../../services/user/user.service';
import { OpenweatherapiService } from '../../services/api/openweatherapi/openweatherapi.service'

import { debounceTime, switchMap } from 'rxjs/operators';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { NgbAlertConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
})

export class SearchComponent implements OnInit {

  public weatherData: iWeather[] = [];

  searchInputCity = new FormControl();
  weather: any = {};

  faStar = faStar;

  constructor( private userService: UserService, 
               private weatherApiService: OpenweatherapiService, 
               private cityService: CityService, 
               private authService: AuthService,
               ngbAlertConfig: NgbAlertConfig
               ) { ngbAlertConfig.animation = false; }
             

  ngOnInit() {
      this.searchInputCity.valueChanges
      .pipe(debounceTime(300),
        switchMap(city =>  this.weatherApiService.getWeatherCity(city)))
      .subscribe(
        res => {
          this.weather['name'] = res.name;
          this.weather['country'] = res['sys'].country;
          this.weather['temp'] = res['main'].temp;
          this.weather['humidity'] = res['main'].humidity;
          this.weather['feels_like'] = res['main'].feels_like;
          this.weather['temp_max'] = res['main'].temp_max;
          this.weather['temp_min'] = res['main'].temp_min;
          this.weather['description'] = res['weather'][0].description;
          this.weather['wind'] = res['wind'].speed;
          this.weather['weather_main'] = res['weather'][0].main;
          console.log(this.weather);
        },
        err => console.log(`Can't get weather. Error code: %s, URL: %s`,
          err.message, err.url)
      )
  }

  cityExist: boolean = false;
  username: any = localStorage.getItem("username");

  getWeather() {

  }

  addFavouriteCity() {
    if(this.weather['name'] != null){
      this.userService.getUserIdByName(this.username.toString()).subscribe(id => {
        
        const credentials = {
          'name': this.weather['name'], // Name of the city
          'userid': Number(id)          // Id from the user
        }

        this.cityService.postFavCity(credentials).subscribe(data => {
          if (data === null) {
            this.cityExist = true;
            this.openPopup();
          }
          else{
            window.location.reload();
          }
        });
        console.log(" City added to favourite cities :) ");
        
      })
    }
  }

  userAuthorized() {
    return this.authService.isUserAuthenticated();
  }

  displayStyle = "none";
  
  openPopup() {
    this.displayStyle = "block";
  }
  
  closePopup() {
    this.displayStyle = "none";
    this.cityExist = false;
    window.location.reload();
  }
}
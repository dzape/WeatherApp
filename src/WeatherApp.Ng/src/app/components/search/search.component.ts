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
import { City } from 'src/app/data/models/city.model';

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

  cities: any;
  cityExist: boolean = false;
  username: any = localStorage.getItem("username");

  addFavouriteCity() {
    if(this.weather['name'] != null){
      this.cityService.getFavouriteCities(this.username).subscribe(citiesResponse => {
        console.log(citiesResponse)
        for (let index = 0; index < citiesResponse.length; index++) {
          const element = citiesResponse[index];
          if(element === this.weather.name.toString()){
            this.cityExist = true;
            this.openPopup();
            break;
          }
        }
        let data: City = { name: this.weather['name'], userusername: this.username};
        this.cityService.postFavCity(data).subscribe(response => console.log(response));
      });
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
  }
}

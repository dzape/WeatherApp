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

  /* Search Form */
  searchInputCity = new FormControl();
  weather: any = {};

  /* Add city to favourite */
  cities: any;

  /* Username of logined user */
  username: any = sessionStorage.getItem("username");

  /* Bootstrap Modal */
  displayStyle = "none";

  /* Icons */
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

  addFavouriteCity() {
    if(this.weather['name'] != null){
      let data: City = { name: this.weather['name'], userusername: this.username};
      this.cityService.postFavCity(data).subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          if(error.status == 201){
            this.openPopup();
          }
          if(error.status == 200){
            window.location.reload();
          }
        }
      );
    }
  }

  userAuthorized() {
    return this.authService.isUserAuthenticated();
  }
 
  openPopup() {
    this.displayStyle = "block";
  }
  
  closePopup() {
    this.displayStyle = "none";
  }
}

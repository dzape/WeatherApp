import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/data/models/city.model';
import { iWeather } from 'src/app/data/models/iweather';
import { OpenweatherapiService } from 'src/app/services/api/openweatherapi/openweatherapi.service';
import { CityService } from 'src/app/services/city/city.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {

  constructor(private http: HttpClient, private userService: UserService, private weatherApiService: OpenweatherapiService, private cityService: CityService) { }
  
  public weatherData: any[] = [];
  cities: any = {};


  ngOnInit() {
    this.getFavouriteCity();
    this.weatherData = [];
  }

  username: any = localStorage.getItem("username");

  getFavouriteCity() {
    this.userService.getUserIdByName(this.username).subscribe((response) => {
      this.cityService.getFavCityes(response).subscribe((res) => {
        this.cities = res;
        if (this.cities.length > 0) {
          for (var i = 0; i <= this.cities.length - 1; i++) {
            console.log(this.cities[i].name);
            this.weatherApiService.getWeather(this.cities[i].name).subscribe((cityWeatherData) => {
              this.weatherData.push(cityWeatherData);
              // this.weatherData[i].name = cityWeatherData.name;
              // this.weatherData[i].temperature = cityWeatherData['main'].temp + ' ℃';
              // this.weatherData[i].humidity = cityWeatherData['main'].humidity + ' %';
              // this.weatherData[i].description = cityWeatherData['weather'][0].description;
              // this.weatherData[i].windspeed = cityWeatherData['wind'].speed + ' km/h'
              console.log(" THIS IS WEATHER DATA",this.weatherData)
            })
          }
          console.log("City Weather Data : ", this.weatherData);
        }
      });
    });
  }

  onClick(city: City){
    console.log(city);
  }

  onDelete(city: City) {
    for (var i = 0; i < this.cities.length; i++) {
      console.log(this.cities[i].name.toString(), " == ", city.name);
      if (city.name === this.cities[i].name) {
        city.id = Number(this.cities[i].id);
        this.cityService.deleteFavCity(city).subscribe((cities) => {
          this.ngOnInit();
        })
        console.log("You have deleted : ", city.name);
      }
    }
  }
}

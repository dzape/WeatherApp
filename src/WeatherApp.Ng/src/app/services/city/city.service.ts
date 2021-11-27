import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


import { City } from '../../data/city/city'
import { OpenweatherapiService } from '../openweatherapi/openweatherapi.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private http: HttpClient) { }

  //get favourite cities (name)
  getFavoriteCities() {
  }

  //add city to favourite
  addFavoriteCities(city: City) {
  }

  // remove favourite city
  deleteFavoriteCity(city: City) {
  }
}

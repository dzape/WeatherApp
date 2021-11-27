import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { debounceTime, switchMap } from 'rxjs/operators';

import { OpenweatherapiService } from 'src/app/services/openweatherapi/openweatherapi.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
})

export class WeatherComponent implements OnInit {

  searchInput = new FormControl();
  weather: any = {};

  constructor(private http: HttpClient, private openweatherService: OpenweatherapiService) { }

  ngOnInit() {
    this.searchInput.valueChanges
      .pipe(debounceTime(200),
        switchMap(city => this.openweatherService.getWeather(city)));
    console.log(this.searchInput);
  }
}

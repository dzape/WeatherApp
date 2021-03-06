import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { faChessBishop, faTrash } from '@fortawesome/free-solid-svg-icons';
import { iWeather } from '../../data/models/iweather';
import { OpenweatherapiService } from '../../services/api/openweatherapi/openweatherapi.service';
import { CityService } from '../../services/city/city.service';
import { MatSort, Sort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { NgbAlertConfig } from '@ng-bootstrap/ng-bootstrap';
import {CdkDragDrop, DropListRef, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

import { City } from 'src/app/data/models/city.model';
import { DataSource } from '@angular/cdk/collections';
import { coerceStringArray } from '@angular/cdk/coercion';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})

export class CityComponent implements AfterViewInit {
  /* Models */
  public weatherData: Array<iWeather> = [];
  public cityData: City[] = [];

  /* Mat Table */
  dataSource: any;
  sortedData: iWeather[] = [];
  displayedColumns: string[] = ['Name', 'Temperature', 'Humidity', 'Description',  'Delete'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild("table") table!: MatTable<iWeather>;

  /* Var */
  cities: any = {};
  city: string = "";

  /* Boostrap Modal */
  displayStyle = "none";

  /* Icons */
  faTrash = faTrash;

  /* SessionStorage */
  username: any = sessionStorage.getItem("username");

  constructor(private weatherApiService: OpenweatherapiService, 
              private cityService: CityService,
              ngbAlertConfig: NgbAlertConfig
              ) { ngbAlertConfig.animation = false; }

  ngAfterViewInit() {
    this.getFavouriteCity();
  }
  
  getFavouriteCity() {
    this.cityService.getFavouriteCities().subscribe((res) => {
      this.cities = res;
      if (this.cities.length > 0) {
        for (var i = 0; i <= this.cities.length - 1; i++) {
          this.weatherApiService.getWeatherCity(this.cities[i]).subscribe((cityWeatherData) => {
            let data: iWeather = {
              name: cityWeatherData.name,
              temperature: cityWeatherData['main'].temp,
              humidity: cityWeatherData['main'].humidity,
              description: cityWeatherData['weather'][0].description,
            }
            this.sortedData.push(data);
            this.dataSource = new MatTableDataSource<iWeather>(this.sortedData);
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.paginator;
          })
        }
      }
    });
  }

  onDelete(city?: string) {
    city = this.city; 
    console.log(city);
    let data: City = { name: city, userusername: this.username}
    this.cityService.deleteFavCity(data).subscribe(
    (city) => 
    {
      console.log(city);
      if(city != null){
        console.log("You have deleted : ", data.name);
      }
    },
    (err) => {
      console.log("ERROR : ",err);
      if(err.status === 200){
        window.location.reload();
      }
    })
  }

  sortData(sort: Sort) {
    const data = this.sortedData.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }
    this.sortedData = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name':
          return compare(a.name, b.name, isAsc);
        case 'temperature':
          return compare(a.temperature, b.temperature, isAsc);
        case 'humidity':
          return compare(a.humidity, b.humidity, isAsc);
        default:
          return 0;
      }
    });
  }

  drop(event: CdkDragDrop<iWeather[]>) {
    const previousIndex = this.dataSource.data.findIndex((row: any) => row === event.item.data);
    moveItemInArray(this.dataSource.data,previousIndex, event.currentIndex);
    this.dataSource.data = this.dataSource.data.slice();
  }

  openPopup(city: string) {
    this.displayStyle = "block";
    this.city = city;
  }
  
  closePopup() {
    this.displayStyle = "none";
  }
}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}

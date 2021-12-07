import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { iWeather } from '../../data/models/iweather';
import { OpenweatherapiService } from '../../services/api/openweatherapi/openweatherapi.service';
import { CityService } from '../../services/city/city.service';
import { UserService } from '../../services/user/user.service';
import { MatSort, Sort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})

export class CityComponent implements AfterViewInit {

  public weatherData: Array<iWeather> = [];
  sortedData: iWeather[] = [];
  faTrash = faTrash;
  data: any;
  dataSource: any;

  constructor(private userService: UserService, 
              private weatherApiService: OpenweatherapiService, 
              private cityService: CityService) 
              { }  

  displayedColumns: string[] = ['Name', 'Temperature', 'Humidity', 'Description', 'Wind Speed', 'Delete'];

  cities: any = {};
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.getFavouriteCity();
  }
  
  username: any = localStorage.getItem("username");

  getFavouriteCity() {
    this.userService.getUserIdByName(this.username).subscribe((response) => {
      this.cityService.getFavCityes(response).subscribe((res) => {
        this.cities = res;
        if (this.cities.length > 0) {
          for (var i = 0; i <= this.cities.length - 1; i++) {
            this.weatherApiService.getWeather(this.cities[i].name).subscribe((cityWeatherData) => {
              let data: iWeather = {
                name: cityWeatherData.name,
                temperature: cityWeatherData['main'].temp,
                humidity: cityWeatherData['main'].humidity,
                description: cityWeatherData['weather'][0].description,
                windspeed: cityWeatherData['wind'].speed
              }
              this.sortedData.push(data);
              this.dataSource = new MatTableDataSource<iWeather>(this.sortedData);
              this.dataSource.paginator = this.paginator;

            })
          }
        }
      });
    });
  }

  onDelete(city: string) {
    for (var i = 0; i < this.cities.length; i++) {
      if (city === this.cities[i].name) {
        let id = Number(this.cities[i].id);
        this.cityService.deleteFavCity(id).subscribe((city) => {
          console.log("You have deleted : ", city);
          window.location.reload();
        })
      }
    }
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
        case 'temp':
          return compare(a.temperature, b.temperature, isAsc);
        case 'humidity':
          return compare(a.humidity, b.humidity, isAsc);
        case 'windspeed':
          return compare(a.windspeed, b.windspeed, isAsc);  
        default:
          return 0;
      }
    });
    this.dataSource = this.sortedData;
  }
}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}


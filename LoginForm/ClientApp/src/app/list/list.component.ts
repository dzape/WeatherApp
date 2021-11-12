import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { ListService } from '../services/list/list.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class ListComponent {
  constructor(private listSrv: ListService, private http: HttpClient) { }

  baseurl: string = 'https://localhost:44306/api/Accounts'
  li: any;
  lis = [];

  listUserById() {
    this.http.get(this.baseurl).subscribe(Response => {
      if (Response === null) {
        error();
      }

      console.log(Response);
      this.li = Response;
      this.lis = this.li.list;

    });

    function error() {
      console.log("Accounts not found.");
    }
  }
}

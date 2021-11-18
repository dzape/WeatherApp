import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ListService } from '../../services/list/list.service'
import { ApiService } from '../../services/api/api.service'

@Component({
  selector: 'app-home',
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {

  constructor(private listSrv: ListService, private http: HttpClient, private api: ApiService) { }

  li: any;
  lis = [];

  ngOnInit() {
    this.http.get(this.api.getApiUrl()).subscribe(Response => {
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

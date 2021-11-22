import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ListService } from '../../services/list/list.service'
import { ApiService } from '../../services/api/api.service'
import { Account } from '../../models/account.model';
import { User } from 'oidc-client';
import { Subscription } from 'rxjs';
import { IAccount } from '../../models/iaccount.model';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  //
  constructor(private listSrv: ListService, private http: HttpClient, private api: ApiService) { }

  errorMessage = 'Something went wrong!';


  li: any;
  lis = [];

  sub!: Subscription;

  accounts: IAccount[] = [];
  filteredProducts: IAccount[] = [];

  ngOnInit() {
    this.li = this.returnUsers();
    this.lis = this.li.list;
  }

  returnUsers() {
    return this.http.get(this.api.getApiUrl() + "accounts").subscribe(response => {
      console.log(response);
      this.li = response;
      console.log(this.li);
    })
  }
}

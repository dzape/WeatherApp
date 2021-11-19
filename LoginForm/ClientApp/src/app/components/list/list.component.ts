import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ListService } from '../../services/list/list.service'
import { ApiService } from '../../services/api/api.service'
import { error } from 'console';
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
    return this.http.get(this.api.getApiUrl()).subscribe(response => {
      this.li = response;
    })
  }
    //this.sub = this.api.getUsers().subscribe({
    //  next: products => {
    //    console.log(products);
    //    this.accounts = products;
    //    this.filteredProducts = this.accounts
    //  },
    //  error: err => this.errorMessage = err
    //});

    //console.log(this.accounts);
    //console.log(this.filteredProducts);   
  
}

import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Account } from '../../models/account.model';

import { ApiService } from '../../services/api/api.service';
import { RegisterService } from '../../services/register/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  acc = new Account();

  constructor(private http: HttpClient,
    private registerService: RegisterService,
    private router: Router
  ) { }

  regForm!: FormGroup;

  userForm = new FormGroup({
    username: new FormControl(this.acc.username,[
      Validators.required,
      Validators.minLength(4)
    ]),
    password: new FormControl(this.acc.password,[
      Validators.required,
      Validators.minLength(6)
    ])
  });

  addAccount() {
    this.registerService.registerNewAccount(this.acc)
      .subscribe(data => {
        if (data === null) {
          alert("Username exist");
        }
        this.router.navigate(["/weather"]);
      })
  }
}


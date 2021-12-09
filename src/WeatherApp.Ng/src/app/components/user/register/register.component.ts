import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Account } from '../../../data/models/account.model';
import { RegisterService } from '../../../services/register/register.service';

import { NgbAlertConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  acc = new Account();
  userAvailable: boolean = false;

  constructor(private http: HttpClient,
    private registerService: RegisterService,
    private router: Router,
    ngbAlertConfig: NgbAlertConfig
  ) { ngbAlertConfig.animation = false; }

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
        if (data != null) {
          this.userAvailable = true;
          this.router.navigate(["/login"]);
        }
      })
  }

  displayStyle = "none";
  
  openPopup() {
    this.displayStyle = "block";
  }
  closePopup() {
    this.displayStyle = "none";
  }
}
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Account } from '../../../data/models/user/account.model';
import { UserService } from '../../../services/user/user.service'

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
    private userService: UserService,
    private router: Router,
    ngbAlertConfig: NgbAlertConfig
  ) { ngbAlertConfig.animation = false; }

  userForm = new FormGroup({
    username: new FormControl(this.acc.username,[
      Validators.required,
      Validators.minLength(4)
    ]),
    email: new FormControl(this.acc.email,[
      Validators.email
    ]),
    password: new FormControl(this.acc.password,[
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(25),
      this.matchValidator('confirmPassword', true)
    ]),
    confirmPassword: new FormControl('',[
      Validators.required,
      this.matchValidator('password')
    ])
  });

  message: string = "";
  addAccount() {
    this.userService.registerNewAccount(this.acc)
      .subscribe(
        (data) => {
          console.log(data);
        },
        (error) => {
          console.log(error.status)
          if(error.status == 200){
            this.userAvailable = true;
          }
        })
  }

  matchValidator(
    matchTo: string, 
    reverse?: boolean
  ): ValidatorFn {
    return (control: AbstractControl): 
    ValidationErrors | null => {
      if (control.parent && reverse) {
        const c = (control.parent?.controls as any)[matchTo] as AbstractControl;
        if (c) {
          c.updateValueAndValidity();
        }
        return null;
      }
      return !!control.parent &&
        !!control.parent.value &&
        control.value === 
        (control.parent?.controls as any)[matchTo].value
        ? null
        : { matching: true };
    };
  }

  displayStyle = "none";
  
  openPopup() {
    this.displayStyle = "block";
  }
  
  closePopup() {
    this.displayStyle = "none";
    if(this.userAvailable){
      this.router.navigate(["/login"]);
    }
  }
}
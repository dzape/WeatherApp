import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
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
      Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
      Validators.minLength(6),
      Validators.maxLength(25),
      this.matchValidator('confirmPassword', true)
    ]),
    confirmPassword: new FormControl('',[
      Validators.required,
      this.matchValidator('password')
    ])
  });

  addAccount() {
    this.registerService.registerNewAccount(this.acc)
      .subscribe(data => {
        if (data != null) {
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
    this.router.navigate(["/login"]);
  }
}
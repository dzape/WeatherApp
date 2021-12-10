import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Account } from 'src/app/data/models/account.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent {
  
  acc = new Account();
  username: any = localStorage.getItem("username");
  userAvailable: boolean = false;

  constructor ( private userService: UserService, private router: Router, private auth: AuthService) {}

  editForm = new FormGroup({
    username: new FormControl(this.acc.username,[
      Validators.required,
      Validators.minLength(4)
    ]),
    password: new FormControl(this.acc.password,[
      Validators.required,
      Validators.minLength(6)
    ])
  });
  
  updateProfile() {
      this.userService.updateUser(this.acc);
      this.userAvailable = true;
      this.logOut();
  }

  deleteUser(){
    this.userService.deleteUser(this.username);
  }

  logOut() {
    this.auth.logOut();
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
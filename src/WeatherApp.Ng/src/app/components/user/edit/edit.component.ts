import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Account } from '../../../data/models/user/account.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserService } from 'src/app/services/user/user.service';
import { UpdateAccModel } from '../../../data/models/user/updateacc.model';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent {
  
  acc = new UpdateAccModel();
  username: any = localStorage.getItem("username");
  userAvailable: boolean = false;

  constructor ( private userService: UserService, private router: Router, private auth: AuthService) {}

  editForm = new FormGroup({
    username: new FormControl(this.acc.newUsername,[
      Validators.required,
      Validators.minLength(4)
    ]),
    password: new FormControl(this.acc.password,[
      Validators.required,
      Validators.minLength(6)
    ])
  });
  
  updateProfile() {
      this.userService.updateUser(this.acc).subscribe(
        (data) => {
          console.log(data);
        },
        (error) => {
          if(error.status == 200){
            this.userAvailable = true;
            this.logOut();
          }
        }
      );
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
  }
}
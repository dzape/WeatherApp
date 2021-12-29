import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Account } from '../../../data/models/user/account.model';
import { AuthService } from '../../../services/auth/auth.service';
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

  constructor ( private userService: UserService,   
                private auth: AuthService) {}

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
        if(data != null){
          console.log(data);
        }
        if(data === 200){
          this.userAvailable = true;
          this.openPopup();
        }
      })
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
    if(this.userAvailable){
      this.logOut();
      window.location.reload();
    }
  }
}
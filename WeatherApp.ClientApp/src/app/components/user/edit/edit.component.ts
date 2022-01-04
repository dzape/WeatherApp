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
  
  info: string = "";
  acc = new UpdateAccModel();
  username: any = localStorage.getItem("username");
  Ok: boolean = false;

  constructor ( private userService: UserService,   
                private auth: AuthService) {}

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
    this.userService.updateUser(this.acc).subscribe(
      (data) => {
        console.log(data.status);
      },
      (err) => {
        console.log(err.status);
        
        if(Number(err.status) === 200){
          this.Ok = true;
          this.info = "Username updated successfully"
          this.openPopup();
        }
        if(Number(err.status) === 201){
          this.info = "Your input is not valid"
          this.openPopup();
        }
        if(Number(err.status) === 202){
          this.info = "Username alreadu exist"
          this.openPopup();
        }
        if(Number(err.status) === 401){
          this.info = "Unauthorized"
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
    if(this.Ok){
      this.logOut();
      window.location.reload();
    }
  }
}
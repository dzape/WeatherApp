import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Account } from 'src/app/data/models/account.model';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent {
  acc = new Account();

  username: any = localStorage.getItem("username");
 
  constructor ( private userService: UserService) {}

  dsa!: FormGroup;
  closeResult!: string;

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
  }

  deleteUser(){
    this.userService.deleteUser(this.username);
  }
}
import {Component, OnInit} from '@angular/core';
import {LoginServiceService} from "../_services/login-service.service";
import {User} from "../models/user";
import {NgIf, TitleCasePipe} from "@angular/common";
import {MatButton} from "@angular/material/button";
import {FormsModule} from "@angular/forms";
import {UsersService} from "../_services/users.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    TitleCasePipe,
    MatButton,
    NgIf,
    FormsModule
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent implements OnInit{
  currentUser: User | undefined
  editUser: boolean = false;
  constructor(
    private loginService: LoginServiceService,
    private userService: UsersService,
    private toastr: ToastrService
    ) {
  }
    ngOnInit(): void {
    this.loadUser()
    }
  loadUser() {
    const userId = localStorage.getItem('userId')

    if (userId)
    this.loginService.getUserFromId(userId).subscribe({
      next: (response) => {
        this.currentUser = response
        this.currentUser.phone
      }
    })
  }

  updateInfo() {
    if (this.currentUser){
      console.log(this.currentUser)
      this.userService.updateUserInfo(this.currentUser.Id, this.currentUser).subscribe({
        next: (response) => {
          this.toastr.info('Settings Changes')
        },
        error: err => this.toastr.error(err.error)
      })
    }
  }

}

import {Component, OnInit} from '@angular/core';
import {LoginServiceService} from "../_services/login-service.service";
import {User} from "../models/user";
import {NgIf, TitleCasePipe} from "@angular/common";
import {MatButton} from "@angular/material/button";
import {FormsModule} from "@angular/forms";
import {UsersService} from "../_services/users.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

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
  resetPassword: boolean = false;
  currentPassword: string  = ''
  newPassword: string = ''
  constructor(
    private loginService: LoginServiceService,
    private userService: UsersService,
    private toastr: ToastrService,
    private router: Router
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

      }
    })
  }

  updateInfo() {
    if (this.currentUser){
      this.userService.updateUserInfo(this.currentUser.Id, this.currentUser).subscribe({
        next: (response) => {
          this.toastr.info('User Info Changed')
          this.editUser = false;
        },
        error: err => {
          this.toastr.error('Error changing info')
        }
      })
    }
  }

  changePassword() {
    if (this.currentUser && this.currentPassword != '' && this.newPassword != '') {
      this.loginService.resetPassword(this.currentUser.Id, this.currentPassword, this.newPassword).subscribe({
        next: (response) => {
          this.toastr.info('Password has been changed')
          this.resetPassword = false;
        },
        error: err => {
          this.toastr.error('Error changing password')
        }
      })

    }
    this.newPassword = ''
    this.currentPassword = ''
  }

}

import {Component, Inject, OnInit} from '@angular/core';
import {OverlayRef} from "@angular/cdk/overlay";
import {JOB_DATA} from "../../../models/edit-job-token";
import {MatButton} from "@angular/material/button";
import { User } from '../../../models/user';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgIf, TitleCasePipe} from "@angular/common";
import {UsersService} from "../../../_services/users.service";
import {ToastrService} from "ngx-toastr";
import {LoginServiceService} from "../../../_services/login-service.service";

@Component({
  selector: 'app-edit-accounts',
  standalone: true,
  imports: [
    MatButton,
    FormsModule,
    NgIf,
    ReactiveFormsModule,
    TitleCasePipe
  ],
  templateUrl: './edit-accounts.component.html',
  styleUrl: './edit-accounts.component.css'
})
export class EditAccountsComponent implements OnInit{
  user: User | undefined

  newPassword: string = '';
  confirmPassword: string = '';
  passwordsMatch: boolean = true;

  changePassword: boolean = false;
  constructor(
    private overlayRef: OverlayRef,
    @Inject(JOB_DATA) public data: any,
    private userService: UsersService,
    private toastr: ToastrService,
    private loginService: LoginServiceService
  ) {}
  ngOnInit(): void {
    this.user = this.data.user
    }
  closeOverlay() {
    this.overlayRef.dispose();
  }
  updateInfo() {
    if (this.user){
      this.userService.updateUserInfo(this.user.Id, this.user).subscribe({
        next: (response) => {
          this.toastr.info('User Info Changed')
        },
        error: err => {
          this.toastr.error('Error Changing info')
        }
      })
    }
  }

  checkPasswords() {
    this.passwordsMatch = this.newPassword === this.confirmPassword;

  }
  resetPassword() {
    if (this.user && this.newPassword != '' && this.newPassword != '') {
      console.log(`${this.user.Id} ${this.newPassword}`)
      this.loginService.forgotPassword(this.user.Id, this.newPassword).subscribe({
        next: () => {
          this.toastr.info('Password has been changed')
          this.changePassword = false;
        },
        error: err => {
          this.toastr.error('Error changing password')
        }
      })

    }
    this.newPassword = ''
    this.confirmPassword = ''
  }

}

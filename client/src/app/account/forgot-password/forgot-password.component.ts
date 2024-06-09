import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {User} from "../../models/user";
import {UsersService} from "../../_services/users.service";
import {NgIf, TitleCasePipe} from "@angular/common";
import {LoginServiceService} from "../../_services/login-service.service";
import {Router, RouterModule} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    TitleCasePipe
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {

  constructor(
    private userService: UsersService,
    private loginService: LoginServiceService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  model: any = {
    username:  ''
  }
  userToReset: User | undefined;

  passwordResetModel = {
    Id: '',
    password: ''
  }
  verifyUser() {
    this.userService.getUserFromUsername(this.model.username).subscribe({
      next: (response: User) => {
        this.userToReset = response
        this.passwordResetModel.Id = response.Id
      }
    })
  }

  resetPassword() {
    this.loginService.forgotPassword(this.passwordResetModel.Id, this.passwordResetModel.password)
      .subscribe({
        next: () =>{
          this.router.navigateByUrl('/login')
          this.toastr.info('Password Changed')
        },

      })
  }
}

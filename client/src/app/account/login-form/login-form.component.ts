import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {loginDto} from '../../dtos/loginDto';
import {LoginServiceService} from '../../_services/login-service.service';
import {Router} from '@angular/router';
import {MatButton} from "@angular/material/button";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButton],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent implements OnInit {
  loading: boolean = false
  showPassword: boolean = false
  model: loginDto = {
    companyId: "",
    userName: '',
    password: '',
    token: ''
  }
  passwordType: string = 'password';

  constructor(private loginService: LoginServiceService, private router: Router, private toastr: ToastrService) {
  }

  ngOnInit(): void {


    let userLogin = localStorage.getItem('user');

    if (userLogin) {
      this.router.navigateByUrl('/jobs');
    }
    let lastCompanyId = localStorage.getItem('companyId');

    if (lastCompanyId) {
      this.model.companyId = lastCompanyId
    }
  }

  clickShowPassword() {
    this.showPassword = !this.showPassword
    if (this.showPassword) {
      this.passwordType = 'text'
    } else {
      this.passwordType = 'password'
    }
  }

  login() {
    if (this.model.password != '' && this.model.userName != '' && this.model.companyId != '') {
      this.loading = true;
      this.loginService.login(this.model).subscribe({
        next: () => {
          this.router.navigateByUrl('/home')
          localStorage.setItem('companyId', this.model.companyId)
          this.loading = false;
        },
        error: (err) => {
          this.toastr.error(err.message)
          this.model.password = ''
          this.loading = false
        }
      });
    } else {

      this.toastr.error('Enter all values to login')
      return;
    }

  }

  resetPassword() {
    this.router.navigateByUrl('account/forgot-password')
  }

  routeToSignup() {
    this.router.navigateByUrl('account/register')
  }
}

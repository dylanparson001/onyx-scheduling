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

  model: loginDto = {
    userName: '',
    password: '',
    token: ''
  }

  constructor(private loginService: LoginServiceService, private router: Router, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    let userLogin = localStorage.getItem('user');

    if (userLogin) {
      this.router.navigateByUrl('/jobs');
    }
  }

  login() {
    this.loginService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/home')
      },
      error: () => {
        this.toastr.error('Incorrect Login')
        this.model.password = ''
      }
    });
  }

  resetPassword() {
    this.router.navigateByUrl('account/forgot-password')
  }

  routeToSignup() {
    this.router.navigateByUrl('account/register')
  }
}

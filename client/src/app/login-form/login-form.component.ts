import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { loginDto } from '../dtos/loginDto';
import { LoginServiceService } from '../_services/login-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {

  model: loginDto = {
    userName: '',
    password: '',
    token: ''
  }

  constructor(private loginService: LoginServiceService, private router: Router) { }

  login() {
    this.loginService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/home')
    });
  }
}

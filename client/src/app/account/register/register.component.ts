import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {Register} from "../../models/register";
import {NgIf} from "@angular/common";
import {ToastrService} from "ngx-toastr";
import {Router, RouterLink} from "@angular/router";
import {LoginServiceService} from "../../_services/login-service.service";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    RouterLink
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(
    private toastr: ToastrService,
    private router: Router,
    private authService: LoginServiceService
  ) {
  }
  model: Register = {
    address: "",
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    city: '',
    role: '',
    phone: '',
    state: '',
    password: ''
  };
  passwordConfirmation: string = ''
  passwordsMatch: boolean = true;
  createAccount() {
    if (this.model.password === '') {
      this.toastr.info('Password must be entered')
      return
    }
    if (!this.passwordsMatch) {
      this.toastr.info('Passwords must match to continue')
      return;
    }

    this.authService.register(this.model).subscribe({
      next: (response) => this.toastr.success('User has been created'),
      error: (error) => this.toastr.error('Error creating user')
    })
  }

  changeOption() {
    console.log(this.model.role)
  }


  checkPasswords() {
    this.passwordsMatch = this.passwordConfirmation === this.model.password;

  }
}

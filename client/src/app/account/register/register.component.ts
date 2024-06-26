import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {Register} from "../../models/register";
import {NgIf} from "@angular/common";
import {ToastrService} from "ngx-toastr";
import {Router, RouterLink} from "@angular/router";
import {LoginServiceService} from "../../_services/login-service.service";
import {User} from "../../models/user";

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
export class RegisterComponent implements OnInit {
  constructor(
    private toastr: ToastrService,
    private router: Router,
    private authService: LoginServiceService) {

  }


  passwordConfirmation: string = ''
  passwordsMatch: boolean = true;

  isAdmin: boolean = false;
  currentUser: User | undefined;
  model: Register = {
    companyId: "",
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
  ngOnInit(): void {
    this.getCurrentUser();

  }

  getCurrentUser() {
    this.authService.getCurrentUser().subscribe({
      next: (response) => {
        this.currentUser = response;

        if (response.role === "Admin") {
          this.isAdmin = true;
        }
        this.model.companyId = this.currentUser.companyId
      }
    })
  }

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
      next: (response) => {
        this.toastr.success('User has been created')
        this.router.navigateByUrl('/account/manage-accounts'
        )
      },
      error: (error) => this.toastr.error(error.error)
    })
  }

  changeOption() {
  }


  checkPasswords() {
    this.passwordsMatch = this.passwordConfirmation === this.model.password;

  }
}

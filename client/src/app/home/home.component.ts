import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginServiceService } from '../_services/login-service.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(public accountService: LoginServiceService, private router: Router) {}

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login')
}

}

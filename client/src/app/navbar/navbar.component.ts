import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  Router,
  RouterLink,
  RouterLinkActive,
  RouterOutlet,
} from '@angular/router';
import { LoginServiceService } from '../_services/login-service.service';
import { FooterComponent } from '../footer/footer.component';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    FooterComponent,
    MatButtonModule,
  ],
})
export class NavbarComponent {
  constructor(
    public authService: LoginServiceService,
    private router: Router
  ) {}

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }
}

import { Component, OnInit } from '@angular/core';
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
import {User} from "../models/user";
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
export class NavbarComponent implements OnInit{
  currentUser: User | null | undefined
  constructor(
    public authService: LoginServiceService,
    private router: Router
  ) {}

  ngOnInit(): void {
  }
  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }


}

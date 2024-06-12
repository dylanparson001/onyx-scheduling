import {Component, Input, OnInit} from '@angular/core';
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
  @Input() currentUser: User | null | undefined
  isAdmin: boolean = false;

  isOffice: boolean = false;
  isField: boolean = false;


  constructor(
    public authService: LoginServiceService,
    private router: Router
  ) {}
  ngOnInit(): void {
    let userId = localStorage.getItem('userId')
    if(userId)
    this.authService.getUserFromId(userId).subscribe({
      next: response => {
        this.currentUser = response
        switch (this.currentUser.role) {
          case 'Admin':
            this.isAdmin = true;
            this.isOffice = false;
            this.isField = false
            break;
          case 'Office':
            this.isOffice = true;
            this.isAdmin = false;
            this.isField = false
            break;
          case 'Field':
            this.isOffice = false;
            this.isAdmin = false;
            this.isField = true;
            break;
          default:
            this.isAdmin = false;
            this.isOffice = false;
            this.isField = false
            break;
        }
      }
    })
  }
  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }


}

import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoginServiceService} from '../_services/login-service.service';
import {Router, RouterLink} from '@angular/router';
import {User} from '../models/user';
import {InvoiceSectionComponent} from "./invoice-section/invoice-section.component";
import {JobSectionComponent} from "./job-section/job-section.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, InvoiceSectionComponent, JobSectionComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  currentUser: string | undefined = ''
  user: User | undefined;
  isAdmin: boolean = false;

  isOffice: boolean = false;
  isField: boolean = false;

  constructor(public accountService: LoginServiceService, private router: Router,
              public authService: LoginServiceService,) {
  }

  ngOnInit(): void {
    const staffString = localStorage.getItem("user");

    // if it doesn't, return from function
    if (!staffString) return;

    // gets employee parsed from string
    const employee: User = JSON.parse(staffString);

    this.accountService.setCurrentUser(employee);

    if (this.currentUser === '' || this.currentUser === null) {
      this.currentUser = localStorage.getItem('userName')!
    }
    let userId = localStorage.getItem('userId')
    if (userId)
      this.authService.getUserFromId(userId).subscribe({
        next: (response: User) => {
          this.user = response
          switch (this.user.role) {
            // Definitely can be done more efficiently
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
          this.accountService.logout();
          this.router.navigateByUrl('/login')
        }

      }

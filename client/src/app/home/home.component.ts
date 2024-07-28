import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginServiceService } from '../_services/login-service.service';
import { Router, RouterLink } from '@angular/router';
import { User } from '../models/user';
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
  currentUser: string | null = ''
  constructor(public accountService: LoginServiceService, private router: Router) {}

  ngOnInit(): void {
    const staffString = localStorage.getItem("user");

    // if it doesn't, return from function
    if(!staffString) return;

    // gets employee parsed from string
    const employee: User = JSON.parse(staffString);

    this.accountService.setCurrentUser(employee);

    if (this.currentUser === '' || this.currentUser === null) {
      this.currentUser = localStorage.getItem('userName')
    }
  }


  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login')
}

}

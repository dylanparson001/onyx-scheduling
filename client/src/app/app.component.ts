import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, NgModelGroup } from '@angular/forms';
import { LoginServiceService } from './_services/login-service.service';
import { User } from './models/user';
import { MatNativeDateModule } from '@angular/material/core';
import {OverlayModule} from "@angular/cdk/overlay";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    NavbarComponent,
    FormsModule,
    MatNativeDateModule,
    OverlayModule
  ],
  providers: [
    DatePipe
  ]
})
export class AppComponent implements OnInit {
  title = 'Onyx Service Management';

  constructor(private accountService: LoginServiceService, ) {}

  ngOnInit() {
    const staffString = localStorage.getItem('user');

    // if it doesn't, return from function
    if (!staffString) return;

    // gets employee parsed from string
    const employee: User = JSON.parse(staffString);

    this.accountService.setCurrentUser(employee);
  }
}

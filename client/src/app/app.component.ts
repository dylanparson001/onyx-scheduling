import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, NgModelGroup } from '@angular/forms';
import { LoginServiceService } from './_services/login-service.service';
import { User } from './models/user';
import { MatNativeDateModule } from '@angular/material/core';
import { MatHint } from '@angular/material/form-field';


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    imports: [CommonModule,
      RouterOutlet,
      RouterLink,
      RouterLinkActive,
      NavbarComponent,
      FormsModule,
      MatNativeDateModule
        ]
})
export class AppComponent implements OnInit {
  title = 'Onyx';
  constructor(private accountService: LoginServiceService){

  }

  ngOnInit() {
    const staffString = localStorage.getItem("user");

    // if it doesn't, return from function
    if(!staffString) return;

    // gets employee parsed from string
    const employee: User = JSON.parse(staffString);

    this.accountService.setCurrentUser(employee);

  }

  convertDateFormat(currentDateTime: Date): string {
    return `${currentDateTime.getMonth()}-${
      currentDateTime.getDay() + 1
    }-${currentDateTime.getFullYear()} ${currentDateTime.getHours()}:${currentDateTime.getMinutes()}:${currentDateTime.getSeconds()}`;
  }
}

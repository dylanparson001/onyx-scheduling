import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, NgModelGroup } from '@angular/forms';
import { LoginServiceService } from './_services/login-service.service';
import { User } from './models/user';
import { MatNativeDateModule } from '@angular/material/core';
import {OverlayModule} from "@angular/cdk/overlay";
import {provideToastr, ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule, provideAnimations} from "@angular/platform-browser/animations";
import {PhoneNumberPipe} from "./phone-number.pipe";
import {FooterComponent} from "./footer/footer.component";
import {BaseChartDirective, provideCharts, withDefaultRegisterables} from "ng2-charts";

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
    OverlayModule,
    FooterComponent,
    BaseChartDirective,
  ],
  providers: [
    DatePipe,
    provideCharts(withDefaultRegisterables()),

  ]
})
export class AppComponent implements OnInit {
  title = 'Onyx Service Management';

  constructor(protected accountService: LoginServiceService, ) {}


  ngOnInit() {
    const staffString = localStorage.getItem('user');

    // if it doesn't, return from function
    if (!staffString) return;

    // gets employee parsed from string
    const employee: User = JSON.parse(staffString);

    this.accountService.setCurrentUser(employee);
  }
}

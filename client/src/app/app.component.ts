import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, NgModelGroup } from '@angular/forms';

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
    ]
})
export class AppComponent {
  title = 'Onyx';
}

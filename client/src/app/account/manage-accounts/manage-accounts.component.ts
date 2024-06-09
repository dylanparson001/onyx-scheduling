import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-manage-accounts',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './manage-accounts.component.html',
  styleUrl: './manage-accounts.component.css'
})
export class ManageAccountsComponent {

}

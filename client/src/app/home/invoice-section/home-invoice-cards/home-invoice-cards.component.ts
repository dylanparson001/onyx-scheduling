import {Component, Input, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Invoice} from "../../../models/invoice";
import {UsersService} from "../../../_services/users.service";
import {User} from "../../../models/user";

@Component({
  selector: 'app-home-invoice-cards',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home-invoice-cards.component.html',
  styleUrl: './home-invoice-cards.component.css'
})
export class HomeInvoiceCardsComponent implements OnInit{
  @Input() invoice: Invoice | undefined
  customer: User | undefined;
  ngOnInit(): void {
        this.getCustomersFromInvoice()
    }
    constructor(private userService: UsersService) {
    }
  getCustomersFromInvoice() {
    if (this.invoice?.assigned_Customer_Id) {
      this.userService
        .getCustomersFromInvoiceId(this.invoice?.assigned_Customer_Id)
        .subscribe({
          next: (response) => {
            this.customer = response;
          },
        });
    }
  }
}

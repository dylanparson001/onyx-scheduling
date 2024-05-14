import {Component, OnInit} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {InvoicesService} from "../../_services/invoices.service";
import {InvoiceCardComponent} from "../../invoice/invoice-card/invoice-card.component";
import {Invoice} from "../../models/invoice";
import {User} from "../../models/user";
import {UsersService} from "../../_services/users.service";
import {invoiceCustomer} from "../../models/invoice-customer";
import {HomeInvoiceCardsComponent} from "./home-invoice-cards/home-invoice-cards.component";

@Component({
  selector: 'app-invoice-section',
  standalone: true,
  imports: [CommonModule, InvoiceCardComponent, HomeInvoiceCardsComponent],
  templateUrl: './invoice-section.component.html',
  styleUrl: './invoice-section.component.css'
})
export class InvoiceSectionComponent implements OnInit {
  todaysDate: any = '';
  todaysInvoices: Invoice[] | undefined;
  todaysCustomers: User[] = [];
  invoiceCustomers: invoiceCustomer[] = [];

  ngOnInit(): void {
    let date = new Date();
    this.todaysDate = this.datePipe.transform(date, "MM-dd-yyyy");

    this.invoiceService.getInvoicesByDate(this.todaysDate, 'Paid', 0, 5).subscribe({
      next: (result) => {
        this.todaysInvoices = result;
      }
    });
  }

  constructor(private datePipe: DatePipe, private invoiceService: InvoicesService, private userService: UsersService) {
  }



}

// for (let i = 0; i < this.todaysInvoices?.length; i++) {
//   console.log(i)
//   const result: invoiceCustomer = {
//     invoice: this.todaysInvoices[i],
//     customer: this.todaysCustomers[i]
//   }
//   this.invoiceCustomers.push(result);
//
// }

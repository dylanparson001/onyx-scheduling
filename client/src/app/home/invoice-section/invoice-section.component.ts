import {Component, OnInit} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {InvoicesService} from "../../_services/invoices.service";
import {InvoiceCardComponent} from "../../invoice/invoice-card/invoice-card.component";
import {Invoice} from "../../models/invoice";
import {User} from "../../models/user";
import {UsersService} from "../../_services/users.service";
import {invoiceCustomer} from "../../models/invoice-customer";

@Component({
  selector: 'app-invoice-section',
  standalone: true,
  imports: [CommonModule, InvoiceCardComponent],
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

    this.invoiceService.getInvoicesByDate(this.todaysDate, 'Open', 0, 5).subscribe({
      next: (result) => {
        this.todaysInvoices = result;
        this.todaysInvoices.forEach(invoice => this.getCustomersFromInvoice(invoice.assigned_Customer_Id));

        result.forEach(x => {
          const result: invoiceCustomer = {
            invoice: x,
            customer: this.getCustomersFromInvoice(x.assigned_Customer_Id)
          }
          console.log(result)
        })

      }
    });
    console.log(this.invoiceCustomers)
  }

  constructor(private datePipe: DatePipe, private invoiceService: InvoicesService, private userService: UsersService) {
  }

  getCustomersFromInvoice(customerId: string) {
    let result: any
    this.userService
      .getCustomersFromInvoiceId(customerId)
      .subscribe({
        next: (response) => {
          result = response;
          console.log(response)
        }
      });
  return result;
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

import { Component, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesService } from '../_services/invoices.service';
import { Invoice } from '../models/invoice';
import { InvoiceCardComponent } from './invoice-card/invoice-card.component';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-invoice',
  standalone: true,
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css',
  imports: [CommonModule, InvoiceCardComponent, FormsModule],
})
export class InvoiceComponent implements OnInit {
  invoices: Invoice[] | undefined;
  model: any = {
    currentDate: '',
  };

  constructor(private invoiceService: InvoicesService,  private router: Router) {}

  ngOnInit() {
    let today = new Date();

    // Get the day of the month
    let dd = today.getDate();

    // Get the month (adding 1 because months are zero-based)
    let mm = today.getMonth() + 1;

    // Get the year
    let yyyy = today.getFullYear();

    // Format the date as mm-dd-yyyy and log it
    let todayString = mm + '-' + dd + '-' + yyyy;

    this.model.currentDate = todayString;

    //this.model.currentDate = '2023-12-11' // for development
    this.getInvoicesByDate();
  }
  getInvoices() {
    this.invoiceService.getAllInvoices().subscribe({
      next: (response: Invoice[]) => {
        this.invoices = response;
      },
      error: (error) => console.log(error),
    });
  }

  getInvoicesByDate() {
    this.invoiceService.getInvoicesByDate(this.model.currentDate).subscribe({
      next: (response: Invoice[]) => {
        this.invoices = response;
        console.log(response);

      },
    });
  }

  routeToNewInvoiceForm() {
    this.router.navigateByUrl('/invoices/new-invoice');
  }
  routeToEditInvoiceForm() {
    this.router.navigateByUrl('/invoices/edit-invoice');
  }
}

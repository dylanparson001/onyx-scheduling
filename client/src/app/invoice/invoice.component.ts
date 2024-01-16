import { Component, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesService } from '../_services/invoices.service';
import { Invoice } from '../models/invoice';
import { InvoiceCardComponent } from './invoice-card/invoice-card.component';
import { FormsModule } from '@angular/forms';
import {
  Router,
  RouterLink,
  RouterLinkActive,
  RouterOutlet,
} from '@angular/router';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-invoice',
  standalone: true,
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css',
  imports: [
    CommonModule,
    InvoiceCardComponent,
    FormsModule,
    MatDatepickerModule,
    MatButtonModule
  ],
})
export class InvoiceComponent implements OnInit {
  invoices: Invoice[] | undefined;
  statusList: string[] | undefined;
  chosenStatus: string = 'Open';

  currentDate: string =  '';

  constructor(
    private invoiceService: InvoicesService,
    private router: Router
  ) {}

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

    this.currentDate = todayString;


    this.loadStatuses();

    console.log(this.chosenStatus)
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
    this.invoiceService.getInvoicesByDate(this.currentDate, this.chosenStatus).subscribe({
      next: (response: Invoice[]) => {
        this.invoices = response;
      },
    });
  }

  routeToNewInvoiceForm() {
    this.router.navigateByUrl('/invoices/new-invoice');
  }
  routeToEditInvoiceForm() {
    this.router.navigateByUrl('/invoices/edit-invoice');
  }

  loadStatuses() {
    this.statusList;
    this.invoiceService.getProcessingStatusOptions().subscribe({
      next: (response) => {
        this.statusList = response;
      },
      error: (error) => console.log(error),
    });
  }
}

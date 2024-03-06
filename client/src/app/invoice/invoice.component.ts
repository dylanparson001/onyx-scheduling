import { Component, OnInit, Output } from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
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
  take: number = 5;
  position: number = 0;
  countOfInvoices: number | undefined;
  totalPages: number = 0;
  currentPage: number = 1;
  currentDate: string | null =  '';

  constructor(
    private invoiceService: InvoicesService,
    private router: Router,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    let today = new Date();

    this.currentDate = this.datePipe.transform(today, 'MM-dd-yyy');

    this.loadStatuses();

    this.getInvoicesByDate();
    this.getCountInvoices();
  }

  getInvoicesByDate() {
    if (this.currentDate != null) {
      this.invoiceService.getInvoicesByDate(this.currentDate, this.chosenStatus, this.position, this.take).subscribe({
        next: (response: Invoice[]) => {
          this.invoices = response;
          this.getCountInvoices();
        },
      });
    }
  }

  resultCountChange() {
    this.setOffsetToZero();
    this.getInvoicesByDate();
  }

  getCountInvoices() {
    if (this.currentDate!= null) {
      this.invoiceService.getCountInvoicesByDate(this.currentDate, this.chosenStatus).subscribe({
        next: (response: number) => {
          this.countOfInvoices = response;
          this.totalPages = Math.ceil(response / this.take)
        }
      })
    }
  }

  setOffsetToZero() {
    this.currentPage = 1
    this.position = 0;
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

  nextButtonClick() {
    if (this.countOfInvoices) {
      if ((this.position + this.take) > this.countOfInvoices) {
        return;
      }
      this.position += this.take;
      this.getInvoicesByDate();
      this.currentPage += 1;
    }
  }
  previousButtonClick() {
    if (this.countOfInvoices) {
      if ((this.position- this.take) < 0) {
        return;
      }
        this.position -= this.take;
        this.getInvoicesByDate();
        this.currentPage -= 1;
    }
  }
}

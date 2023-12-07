import { Component, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesService } from '../_services/invoices.service';
import { Invoice } from '../models/invoice';
import { InvoiceCardComponent } from "./invoice-card/invoice-card.component";
import { Observable } from 'rxjs';
@Component({
    selector: 'app-invoice',
    standalone: true,
    templateUrl: './invoice.component.html',
    styleUrl: './invoice.component.css',
    imports: [CommonModule, InvoiceCardComponent]
})
export class InvoiceComponent implements OnInit {
  invoices: Invoice[] | undefined

  constructor(private invoiceService: InvoicesService) {}

  ngOnInit() {
     this.getInvoices();
  }
   getInvoices() {
     this.invoiceService.getAllInvoices().subscribe({
      next: (response: Invoice[]) => {
        this.invoices = response
      },
      error: (error) => console.log(error)
    });
  }

}

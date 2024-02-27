import {Component, OnInit} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {InvoicesService} from "../../_services/invoices.service";
import {InvoiceCardComponent} from "../../invoice/invoice-card/invoice-card.component";

@Component({
  selector: 'app-invoice-section',
  standalone: true,
  imports: [CommonModule, InvoiceCardComponent],
  templateUrl: './invoice-section.component.html',
  styleUrl: './invoice-section.component.css'
})
export class InvoiceSectionComponent implements OnInit{
  todaysDate: any = '';
  todaysInvoices: any | null;
  ngOnInit(): void {
      let date = new Date();
      this.todaysDate = this.datePipe.transform(date, "MM-dd-yyyy");
      this.todaysInvoices = this.invoiceService.getInvoicesByDate(this.todaysDate, 'Open').subscribe({
        next: (result) => {
          this.todaysInvoices = result;
          console.log(result);
    }
      });
  }

    constructor(private datePipe: DatePipe, private invoiceService: InvoicesService) {
    }
}

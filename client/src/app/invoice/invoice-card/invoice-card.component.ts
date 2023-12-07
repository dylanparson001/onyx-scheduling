import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesService } from '../../_services/invoices.service';
import { Invoice } from '../../models/invoice';

@Component({
  selector: 'app-invoice-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoice-card.component.html',
  styleUrl: './invoice-card.component.css'
})
export class InvoiceCardComponent implements OnInit {
  @Input() invoice: Invoice | undefined

  items: any
  constructor(private invoiceService: InvoicesService) {}


  ngOnInit(): void {
    console.log(this.invoice)
  }

}

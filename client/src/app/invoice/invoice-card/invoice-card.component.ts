import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesService } from '../../_services/invoices.service';
import { Invoice } from '../../models/invoice';
import { User } from '../../models/user';
import { UsersService } from '../../_services/users.service';

@Component({
  selector: 'app-invoice-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoice-card.component.html',
  styleUrl: './invoice-card.component.css',
})
export class InvoiceCardComponent implements OnInit {
  @Input() invoice: Invoice | undefined;

  items: any;
  constructor(
    private invoiceService: InvoicesService,
    private userService: UsersService
  ) {}
  customer: User | undefined;

  ngOnInit(): void {
    this.getCustomersFromInvoice();
    this.getInvoiceItems();
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

  getInvoiceItems() {
    if (this.invoice) {
      this.invoiceService.getInvoiceItems(this.invoice.id).subscribe({
        next: (response) => {
          this.items = response;
        },
      });
    }
  }
}

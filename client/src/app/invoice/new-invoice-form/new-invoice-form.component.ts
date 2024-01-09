import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UsersService } from '../../_services/users.service';
import { User } from '../../models/user';
import { Invoice } from '../../models/invoice';
import { InvoicesService } from '../../_services/invoices.service';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatHint } from '@angular/material/form-field';

@Component({
  selector: 'app-new-invoice-form',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatDatepickerModule],
  templateUrl: './new-invoice-form.component.html',
  styleUrl: './new-invoice-form.component.css',
})
export class NewInvoiceFormComponent implements OnInit {
  existingCustomers: User[] | undefined;
  technicianList: User[] | undefined;
  selectedCustomer: User | undefined;
  selectedTech: User | undefined;


  invoice: Invoice = {
    id: 0,
    address: '',
    city: '',
    invoiceNumber: '',
    assigned_Customer_Id: '',
    assigned_Technician_Id: '',
    total_Price: 0.0,
    invoiceInvoice_Items: [],
    createdDateTime: '',
    finishedDateTime: '',
    scheduledStartDateTime: '',
    scheduledEndDateTime: '',
  };

  constructor(
    private userService: UsersService,
    private router: Router,
    private invoiceService: InvoicesService
  ) {}

  ngOnInit() {
    let todaysDate = new Date();

    //ng-this.invoice.scheduledStartDateTime = todaysDate.getTime().toString();

    this.loadExistingCustomers();
    this.loadTechnicians();
  }

  inputSelectedCustomerInfo() {
    if (this.selectedCustomer) {
      this.invoice.assigned_Customer_Id = this.selectedCustomer.id;
      this.invoice.address = this.selectedCustomer.address;
      this.invoice.city = this.selectedCustomer.city;
    }
  }

  loadExistingCustomers() {
    this.userService.getAllCustomers().subscribe({
      next: (response: User[]) => {
        this.existingCustomers = response;
      },
    });
  }

  loadTechnicians() {
    this.userService.getAllTechnicians().subscribe({
      next: (response: User[]) => {
        this.technicianList = response;
      },
    });
  }

  submitForm() {
    let currentDateTime = new Date();
    console.log(currentDateTime)
    let currentDateString = this.convertDateFormat(currentDateTime);
    this.invoice.createdDateTime = currentDateString;
    console.log(currentDateString);
    this.invoiceService.postInvoice(this.invoice).subscribe({
      next: () => this.router.navigateByUrl('/invoices')
    });
  }

  cancelForm() {
    this.router.navigateByUrl('/invoices');
  }

  convertDateFormat(currentDateTime: Date): string {
    return `${currentDateTime.getMonth()+1}-${
      currentDateTime.getDate()
    }-${currentDateTime.getFullYear()} ${currentDateTime.getHours()}:${currentDateTime.getMinutes()}:${currentDateTime.getSeconds()}`;
  }
}

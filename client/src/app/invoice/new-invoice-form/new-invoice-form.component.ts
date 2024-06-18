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
import { DatePipe } from '@angular/common';
import { ProcessingStatus } from '../../enums/ProcessingStatus';
import {DateServiceService} from "../../_services/date-service.service";

@Component({
  selector: 'app-new-invoice-form',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatDatepickerModule],
  providers: [DatePipe],
  templateUrl: './new-invoice-form.component.html',
  styleUrl: './new-invoice-form.component.css',
})
export class NewInvoiceFormComponent implements OnInit {
  existingCustomers: User[] | undefined;
  technicianList: User[] | undefined;
  selectedCustomer: User | undefined;
  selectedTech: User | undefined;
  statusList: ProcessingStatus[] = [];
  scheduledStartTime: string = '';
  scheduledEndTime: string = '';
  model: any;
  invoice: Invoice = {
    companyId: "",
    id: 0,
    address: '',
    city: '',
    invoiceNumber: '',
    assigned_Customer_Id: '',
    assigned_Technician_Id: '',
    total_Price: 0.0,
    processing_Status: ProcessingStatus.Open,
    invoiceInvoice_Items: [],
    createdDateTime: '',
    finishedDateTime: '',
    scheduledStartDateTime: '',
    scheduledEndDateTime: ''
  };

  constructor(
    private userService: UsersService,
    private router: Router,
    private invoiceService: InvoicesService,
    private datePipe: DatePipe,
    private dateService: DateServiceService
  ) {}

  ngOnInit() {
    //ng-this.invoice.scheduledStartDateTime = todaysDate.getTime().toString();
    this.loadStatuses();
    this.loadExistingCustomers();
    this.loadTechnicians();
  }

  inputSelectedCustomerInfo() {
    if (this.selectedCustomer) {
      this.invoice.assigned_Customer_Id = this.selectedCustomer.Id;
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

    this.invoice.createdDateTime = this.convertDateFormat(currentDateTime);

    let scheduledStartDate = new Date(this.invoice.scheduledStartDateTime);
    let scheduledEndDate = new Date(this.invoice.scheduledStartDateTime);


    let completeStartDate = this.dateService.returnDateTime(
      scheduledStartDate,
      this.scheduledStartTime
    );

    let completeEndDate = this.dateService.returnDateTime(
      scheduledEndDate,
      this.scheduledEndTime
    );


    this.invoice.scheduledStartDateTime = this.convertDateFormat(completeStartDate);
    this.invoice.scheduledEndDateTime = this.convertDateFormat(completeEndDate);

    this.invoiceService.postInvoice(this.invoice).subscribe({
      next: () => this.router.navigateByUrl('/invoices'),
    });
  }


  cancelForm() {
    this.router.navigateByUrl('/invoices');
  }

  // returns date formatted to send to api
  convertDateFormat(currentDateTime: Date): string {
    return `${currentDateTime.getMonth() + 1}-${currentDateTime.getDate()}-${currentDateTime.getFullYear()} ${currentDateTime.getHours()}:${currentDateTime.getMinutes()}:${currentDateTime.getSeconds()}`;
  }

  // checks for am/pm and adjusts hours properly
  convertTimeFormat(chosenTime: string): string {
    let splitTime;
    let splitAmPm;

    // split array at :
    splitTime = chosenTime.split(':');
    // split array at space
    splitAmPm = splitTime[1].split(' ');

    // remove indexes from original array
    splitTime.splice(1, 1);

    splitTime.push(splitAmPm[0]);
    splitTime.push(splitAmPm[1]);

    let hourMark = parseInt(splitTime[0]);
    if (splitTime.includes('PM')) {
      hourMark = hourMark + 12;
    }

    let timeResult = `${hourMark.toString()}:${splitTime[1]}:00`;
    return timeResult;
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

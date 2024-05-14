import {Component, OnInit} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {HttpClient} from "@angular/common/http";
import {JobsService} from "../../_services/jobs.service";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {User} from "../../models/user";
import {ProcessingStatus} from "../../enums/ProcessingStatus";
import {Invoice} from "../../models/invoice";
import {UsersService} from "../../_services/users.service";
import {Router} from "@angular/router";
import {InvoicesService} from "../../_services/invoices.service";
import {Jobs} from "../../models/jobs";
import {InvoiceItems} from "../../models/invoice-items";
import {ItemsService} from "../../_services/items.service";
import {Item} from "../../models/item";

@Component({
  selector: 'app-new-job-form',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatDatepickerModule, ReactiveFormsModule],
  templateUrl: './new-job-form.component.html',
  styleUrl: './new-job-form.component.css'
})
export class NewJobFormComponent implements OnInit{

  existingCustomers: User[] | undefined;
  technicianList: User[] | undefined;
  selectedCustomer: User | undefined;
  selectedTech: User | undefined;
  statusList: string[] = [];
  scheduledStartTime: string = '';
  scheduledEndTime: string = '';
  model: any;
  invoiceItems: InvoiceItems = {
    id: 0,
    categoryId: 0,
    itemName: '',
    price: 0
  }

  job: Jobs = {
    id: 0,
    address: '',
    city: '',
    processingStatus: '',
    createdDateTime: '',
    finishedDateTime: '',
    scheduledStartDateTime: '',
    scheduledEndDateTime: '',
    assignedTechnicianId: '',
    assignedCustomerId: '',
    totalPrice: 0,
    invoiceNumber: 0,
    invoiceId: 0,
    invoiceItems: []
  }
  categories: any

  items: Item[] = [];


  constructor(
    private userService: UsersService,
    private router: Router,
    private invoiceService: InvoicesService,
    private jobService: JobsService,
    private itemService: ItemsService,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.itemService.getItems().subscribe({
      next: result => {
        this.items = result;
      }
    }
    );

    this.loadExistingCustomers();
    this.loadTechnicians();
    this.loadJobStatusList();
    }
  inputSelectedCustomerInfo() {
    if (this.selectedCustomer) {
      this.job.assignedCustomerId = this.selectedCustomer.id;
      this.job.address = this.selectedCustomer.address;
      this.job.city = this.selectedCustomer.city;
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

  loadJobStatusList() {
    this.jobService.getJobStatusList().subscribe({
      next: (response) => {
        this.statusList = response

      }
    })
  }
  submitForm() {
    let currentDateTime = new Date();

    this.job.createdDateTime = this.convertDateFormat(currentDateTime);
    let scheduledStartDate = new Date(this.job.scheduledStartDateTime);
    let scheduledEndDate = new Date(this.job.scheduledStartDateTime);


    let completeStartDate = this.returnDateTime(
      scheduledStartDate,
      this.scheduledStartTime
    );

    let completeEndDate = this.returnDateTime(
      scheduledEndDate,
      this.scheduledEndTime
    );


    this.job.scheduledStartDateTime = this.convertDateFormat(completeStartDate);
    this.job.scheduledEndDateTime = this.convertDateFormat(completeEndDate);

    console.log(this.job)
    this.jobService.postJob(this.job).subscribe({
      next: value => this.router.navigateByUrl('jobs/')
    })
  }
  convertDateFormat(currentDateTime: Date): string {
    return `${currentDateTime.getMonth() + 1}-${currentDateTime.getDate()}-${currentDateTime.getFullYear()} ${currentDateTime.getHours()}:${currentDateTime.getMinutes()}:${currentDateTime.getSeconds()}`;
  }
  returnDateTime(date: Date, time: string) {
    let timeArray = time.split(':');
    let monthString;
    let monthInt = date.getMonth() + 1;
    let dayInt = date.getDate();
    let dayString;

    if (dayInt < 10) {
      dayString = `0${dayInt}`;
    } else {
      dayString = `${dayInt}`;
    }

    if (monthInt < 10) {
      monthString = `0${monthInt}`;
    } else {
      monthString = `${monthInt}`;
    }

    let returnDate = new Date(`${date.getFullYear()}-${monthString}-${dayString}T${timeArray[0]}:${timeArray[1]}:00`
    );

    return returnDate;
  }

  cancelForm() {
    this.router.navigateByUrl('/jobs');
  }
  // getAllItems() {
  //   this.jobService.getItems().subscribe({
  //       next: (response) => {
  //         this.items = response
  //       }
  //     }
  //   );
  // }

  // getAllCategories() {
  //   this.jobService.getCategories().subscribe({
  //     next: (response) => {
  //       this.categories = response
  //     }
  //   })
  // }
}
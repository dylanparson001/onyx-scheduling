import {Component, OnInit, Output, ViewChild} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {JobsService} from "../_services/jobs.service";
import {HomeInvoiceCardsComponent} from "../home/invoice-section/home-invoice-cards/home-invoice-cards.component";
import {MatButtonModule} from "@angular/material/button";
import {Router, RouterLink, RouterLinkActive} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {InvoiceCardComponent} from "../invoice/invoice-card/invoice-card.component";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {Invoice} from "../models/invoice";
import {InvoicesService} from "../_services/invoices.service";
import {User} from "../models/user";
import {JobCardComponent} from "./job-card/job-card.component";
import {UsersService} from "../_services/users.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-jobs.ts',
  standalone: true,
  imports: [CommonModule, HomeInvoiceCardsComponent, MatButtonModule, RouterLink, RouterLinkActive, FormsModule, InvoiceCardComponent, MatDatepickerModule, ReactiveFormsModule, JobCardComponent],
  templateUrl: './jobs.component.html',
  styleUrl: './jobs.component.css'
})
export class JobsComponent implements OnInit {
  todaysJobs: any;
  invoices: Invoice[] | undefined;
  statusList: string[] | undefined;
  chosenStatus: string = 'Paid'; // default value
  take: number = 5;
  position: number = 0;
  countOfInvoices: number | undefined;
  totalPages: number = 0;
  currentPage: number = 1;
  @Output() currentDate: any = '';
  @Output() technician: any;
  techList: any[] = [];
  today: Date = new Date();
  @ViewChild(JobCardComponent) jobCard!: JobCardComponent

  ngOnInit(): void {
    let today = this.today

    this.getAllTechnicians();

    this.currentDate = this.datePipe.transform(today, 'MM-dd-yyy');
    this.loadStatuses();
  }

  constructor(private jobService: JobsService,
              private invoiceService: InvoicesService,
              private router: Router,
              private datePipe: DatePipe,
              private userService: UsersService) {
  }

  getAllTechnicians() {
    this.userService.getAllTechnicians().subscribe({
      next: (response) => {
        this.techList = response;
      }
    })
  }
  reloadJobs() {
    // this.jobCard.getJobs();
  }
  routeToNewJobForm() {
    this.router.navigateByUrl('jobs/new-job')
  }

  loadStatuses() {
    this.jobService.getJobStatusList().subscribe({
      next: (response) => {
        this.statusList = response;
      },
      error: (error) => console.log(error),
    });
  }

  nextButtonClick() {
    if (this.countOfInvoices) {
      if ((this.currentPage + 1) > this.totalPages) {
        return;
      }
      this.position += this.take;
      this.currentPage += 1;
    }
  }
  previousButtonClick() {
    if (this.countOfInvoices) {
      if ((this.position- this.take) < 0) {
        return;
      }
      this.position -= this.take;
      this.currentPage -= 1;
    }
  }
}

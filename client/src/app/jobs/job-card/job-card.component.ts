import {Component, Inject, Injector, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {JobsService} from "../../_services/jobs.service";
import {InvoicesService} from "../../_services/invoices.service";
import {Router} from "@angular/router";
import {User} from "../../models/user";
import {Jobs} from "../../models/jobs";
import {UsersService} from "../../_services/users.service";
import {MatButtonModule} from "@angular/material/button";
import {EditJobComponent} from "../edit-job/edit-job.component";
import {Overlay, OverlayRef} from "@angular/cdk/overlay";
import {ComponentPortal} from "@angular/cdk/portal";
import {JOB_DATA} from "../../models/edit-job-token";
import {ItemsService} from "../../_services/items.service";

@Component({
  selector: 'app-job-card',
  standalone: true,
  imports: [CommonModule, MatButtonModule, EditJobComponent],
  templateUrl: './job-card.component.html',
  styleUrl: './job-card.component.css'
})
export class JobCardComponent implements OnChanges {

  @Input() currentDate: string | null | undefined;
  @Input() tech: User | null | undefined;
  chosenDate: string | null = '';
  jobs: Jobs[] | undefined;
  customers: { [jobId: string]: User } = {}; // Use a dictionary to store customers by job ID
  technician: User | undefined;
  editJob: boolean = false;
  private overlayRef: OverlayRef | undefined;


  ngOnChanges(changes: SimpleChanges): void {
    this.getJobs();
  }

  constructor(private jobService: JobsService,
              private invoiceService: InvoicesService,
              private router: Router,
              private datePipe: DatePipe,
              private userService: UsersService,
              private overlay: Overlay,
              private injector: Injector,
              private itemService: ItemsService
  ) { }

  openJobOverlay(job: Jobs, customer: User) {
    const positionStrategy = this.overlay.position()
      .global()
      .centerVertically()
      .centerHorizontally()

    this.overlayRef = this.overlay.create({
      positionStrategy: positionStrategy
    });
    const data = { job, customer, tech:this.tech };

    const injector = Injector.create({
      providers: [
        { provide: OverlayRef, useValue: this.overlayRef },
        { provide: JOB_DATA, useValue: data }
      ],
      parent: this.injector
    });

    const editOverlay = new ComponentPortal(
      EditJobComponent, null, injector);

    this.overlayRef.attach(editOverlay);

  }

  loadItems() {
    if(this.jobs) {
      this.jobs.forEach(x => {
        this.jobService.getItemsFromJob(x.id).subscribe({
          next: response => {
            x.invoiceItems = response
            console.log(response)
          }

        })
      })
    }
  }

  getJobs() {
    if (this.jobs) {
      this.jobs.length = 0;
    }

    this.chosenDate = this.datePipe.transform(this.currentDate, 'MM-dd-yyyy HH:mm')

    if (this.chosenDate != null && this.tech != null) {
      this.jobService.getJobsByTechAndDate(this.chosenDate, this.tech.id).subscribe({
        next: (response) => {
          this.jobs = response
          // Sort jobs by startDate (assuming startDate is a string in the format 'yyyy-MM-dd' or similar)
          this.jobs.sort((a, b) => new Date(a.scheduledStartDateTime).getTime() - new Date(b.scheduledStartDateTime).getTime());
          this.loadItems()
          this.getCustomerInfo()
        }
      })
    }
  }

  getCustomerInfo() {
    if (this.jobs) {
      this.customers = {}; // Reset the customers dictionary
      this.jobs.forEach(job => {
        this.userService.getCustomersFromInvoiceId(job.assigned_Customer_Id).subscribe({
          next: (response) => {
            this.customers[job.id] = response; // Store customer by job ID
          }
        })
      })
    }
  }

  // Method to get customer for a specific job
  getCustomerForJob(job: Jobs): User | undefined {
    return this.customers[job.id];
  }

  // Method to get the CSS class for a job based on its processing status
  getJobCardClass(status: string): string {
    switch (status) {
      case 'Open':
        return 'card-open';
      case 'Pending':
        return 'card-pending';
      case 'Started':
        return 'card-started';
      case 'Cancelled':
        return 'card-cancelled'
      case 'Closed':
        return 'card-closed';
      default:
        return '';
    }
  }

  showEditJob(job: Jobs, customer: User | undefined) {
    if (customer)
      this.openJobOverlay(job, customer);
  }

}

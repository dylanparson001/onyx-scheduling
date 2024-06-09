import {Component, Inject, Input, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Jobs} from "../../models/jobs";
import {User} from "../../models/user";
import {MatButtonModule} from "@angular/material/button";
import {OverlayRef} from "@angular/cdk/overlay";
import {JOB_DATA} from "../../models/edit-job-token";
import {ItemsService} from "../../_services/items.service";
import {Item} from "../../models/item";
import {FormsModule} from "@angular/forms";
import {JobsService} from "../../_services/jobs.service";
import {ToastrService} from "ngx-toastr";
import {DateServiceService} from "../../_services/date-service.service";
import {UsersService} from "../../_services/users.service";

@Component({
  selector: 'app-edit-job',
  standalone: true,
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './edit-job.component.html',
  styleUrl: './edit-job.component.css'
})
export class EditJobComponent implements OnInit {
  job: Jobs | undefined;
  customer: User | undefined;
  technician: User | undefined;
  technicianList: User[] = []
  items: Item[] = [];
  editedJob: Jobs | undefined;
  chosenItems: Item[] = [];
  chosenItemIds: number[] = []
  selectedItem: Item | undefined;
  totalPrice: number = 0.0;
  removingItems: boolean = false;
  editingItems: boolean = false;
  jobStatuses: string[] = [];
  newStatus: string = '';
  newScheduledStartTime: string = '';
  newScheduledEndTime: string = '';

  constructor(
              private overlayRef: OverlayRef,
              @Inject(JOB_DATA) public data: any,
              private itemService: ItemsService,
              private jobService: JobsService,
              private toastr: ToastrService,
              private dateService: DateServiceService,
              private userService: UsersService
  ) {
  }

  ngOnInit(): void {
    this.loadJobAndCustomer()
    this.loadItems()
    this.loadItemsFromJob()
    this.loadJobStatuses()
    this.loadAllTechs()
  }
  loadAllTechs() {
    this.userService.getAllTechnicians().subscribe({
      next: (response) => {
        this.technicianList = response
      }
    })
  }

  loadJobStatuses() {
    this.jobService.getJobStatusList().subscribe({
      next: (response) => {
        this.jobStatuses = response
      }
    })
  }

  loadItems() {
    this.itemService.getItems().subscribe({
      next: (response) => {
        this.items = response
        this.items.forEach(x => x.quantity = x.defaultQuantity)
      }
    })
  }

  loadItemsFromJob() {
    if (this.job)
      this.jobService.getItemsFromJob(this.job?.id).subscribe({
        next: (response) => this.chosenItems = response
      })
    }

  updateItems() {
    if (this.selectedItem) {
      this.chosenItems.push(this.selectedItem)
      this.chosenItemIds.push(this.selectedItem?.id)
    }
  }

  updateJobItems() {
    if (this.job?.id){
      for (let i = 0; i < this.chosenItems.length; i++) {
        if (this.chosenItems[i].quantity === 0){
          this.toastr.error('Item quantity cannot be 0')
          return;
        }
      }

      this.jobService.postItems(this.chosenItems, this.job?.id).subscribe({
        next: () => this.toastr.success('Items added successfully!')
      });
    }
  }

  loadJobAndCustomer() {
    this.job = this.data.job
    this.customer = this.data.customer;
    this.technician = this.data.tech
  }

  closeOverlay() {
    this.overlayRef.dispose();
  }

  submitChanges() {
    this.updateJobItems()
  }

  updateItemQuantity(chosenItem: Item) {
    console.log(chosenItem.quantity)
  }

  removeItem(chosenItem: Item) {
    if(this.job)
    this.jobService.removeItemsFromJob(this.job?.id, chosenItem.id).subscribe({
      next: value => {
        this.toastr.success('Item removed')
      }
    });


    this.chosenItems = this.chosenItems.filter(x => x.id != chosenItem.id);
  }

  updateJobStatus() {
    if (this.newStatus === '') {
      this.toastr.error('Status must be chosen')
      return;
    }
    if(this.job) {
      this.jobService.changeProcessingStatus(this.job.id, this.newStatus).subscribe({
        next: response => {
          this.toastr.success('Status Changed')
          this.editingItems = false;
          // this.loadJobChanges()
        }
      })
    }
  }

  loadJobChanges() {
    if (this.job)
    this.jobService.getJobByJobId(this.job?.id).subscribe({
      next: response => {
        this.job = response
      }
    })
  }

  completeJob() {
    if(this.job){
      if (this.job.processing_Status != "Closed") {
        let confirmation = confirm("Are you sure you want to close this job?")

        if (!confirmation) {
          return;
        }
      }

      this.jobService.closeJob(this.job?.id).subscribe({
        next: value => {
          this.toastr.success('Job closed')
          this.closeOverlay()
      },
        error: err => this.toastr.error(err.error)
      });
      }
  }

  onTimeChange(event: string, type: string): void {
    const time = this.roundToNearest15Minutes(event);
    if (type === 'startTime') {
      this.newScheduledStartTime = time;
    } else if (type === 'endTime') {
      this.newScheduledEndTime= time;
    }
  }

  roundToNearest15Minutes(time: string): string {
    const [hours, minutes] = time.split(':').map(Number);
    const totalMinutes = hours * 60 + minutes;
    const roundedMinutes = Math.round(totalMinutes / 15) * 15;
    const roundedHours = Math.floor(roundedMinutes / 60);
    const remainderMinutes = roundedMinutes % 60;

    const paddedHours = String(roundedHours).padStart(2, '0');
    const paddedMinutes = String(remainderMinutes).padStart(2, '0');

    return `${paddedHours}:${paddedMinutes}`;
  }
  convertDateFormat(currentDateTime: Date): string {
    return `${currentDateTime.getMonth() + 1}-${currentDateTime.getDate()}-${currentDateTime.getFullYear()} ${currentDateTime.getHours()}:${currentDateTime.getMinutes()}:${currentDateTime.getSeconds()}`;
  }

  updateJobTime() {
    if (this.job){
      let dateTimeStartTemp = new Date(this.job.scheduledStartDateTime);
      let formattedStartDate = this.dateService.returnDateTime(dateTimeStartTemp, this.newScheduledStartTime);
      let formattedStringStartDate: string = this.convertDateFormat(formattedStartDate);

      let dateTimeEndTemp = new Date(this.job.scheduledEndDateTime);
      let formattedEndDate = this.dateService.returnDateTime(dateTimeEndTemp, this.newScheduledEndTime);
      let formattedStringEndDate: string = this.convertDateFormat(formattedEndDate);

      this.jobService.updateJobTime(this.job?.id, formattedStringStartDate, formattedStringEndDate).subscribe({
        next: (response) => {
          this.toastr.info('Job time updated')
        },
        error: (err) => {
          this.toastr.error(err.error)
        }
      })
    }
  }
}

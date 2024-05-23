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
  items: Item[] = [];
  editedJob: Jobs | undefined;
  chosenItems: Item[] = [];
  chosenItemIds: number[] = []
  selectedItem: Item | undefined;
  totalPrice: number = 0.0;

  constructor(private overlayRef: OverlayRef,
              @Inject(JOB_DATA) public data: any,
              private itemService: ItemsService,
              private jobService: JobsService
  ) {
  }

  ngOnInit(): void {
    this.loadJobAndCustomer()
    this.loadItems()
    this.loadItemsFromJob()
  }

  loadItems() {
    this.itemService.getItems().subscribe({
      next: (response) => this.items = response

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
    if (this.job?.id)
      this.jobService.postItems(this.chosenItems, this.job?.id).subscribe({
        next: () => console.log('successfully added items')
      });
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
    });


    this.chosenItems = this.chosenItems.filter(x => x.id != chosenItem.id);
  }
}

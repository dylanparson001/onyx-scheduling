import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Item} from "../../models/item";
import {ItemsService} from "../../_services/items.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './item-card.component.html',
  styleUrl: './item-card.component.css'
})
export class ItemCardComponent implements OnInit{

  @Input() removingItems: boolean | undefined
  @Input() item: Item | undefined
  itemDeleted: boolean = false
  @Output() itemRemoved = new EventEmitter<boolean>();

  countOfItems: number | undefined;
  totalPages: number = 0;
  currentPage: number = 1;
  constructor(private itemService: ItemsService,
              private toastr: ToastrService
              ) {}
  ngOnInit() {

  }

  deleteItem() {
    if (this.item) {
      this.itemService.deleteItem(this.item?.id).subscribe({
        next: () => {

          this.toastr.info('Item deleted')
          this.itemRemoved.emit(true)
        }
      })
    }
  }
}

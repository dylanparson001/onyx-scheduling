import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Item} from "../../models/item";
import {CurrencyPipe, NgIf} from "@angular/common";
import {Category} from "../../models/category";
import {ItemsService} from "../../_services/items.service";

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [
    CurrencyPipe,
    NgIf
  ],
  templateUrl: './category-card.component.html',
  styleUrl: './category-card.component.css'
})
export class CategoryCardComponent {
  @Input() removingCategories: boolean | undefined
  @Input() category: Category | undefined
  itemDeleted: boolean = false
  @Output() categoryRemoved = new EventEmitter<boolean>();

  constructor(private itemService: ItemsService) {
  }

  deleteCategory() {
    if (this.category) {

      this.itemService.deleteCategory(this.category.id).subscribe({
        next: () => {
          this.categoryRemoved.emit(true);
        }
      })
    }
  }
}

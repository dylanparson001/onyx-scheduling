import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgForOf} from "@angular/common";
import {Item} from "../../models/item";
import {Category} from "../../models/category";
import {ItemsService} from "../../_services/items.service";

@Component({
  selector: 'app-new-item',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf
  ],
  templateUrl: './new-item.component.html',
  styleUrl: './new-item.component.css'
})
export class NewItemComponent {
  items: Item[] = []
  categories: Category[] = [];
  chosenCategoryId: number = 0;

  constructor(
    private itemService: ItemsService
  ) {}


  ngOnInit(): void {
    this.loadItemsByCategory()
    this.loadCategories()
  }

  loadItems() {
    this.itemService.getItems().subscribe({
      next: (response) => {
        this.items = response
      }
    })
  }

  loadItemsByCategory() {
    if (this.chosenCategoryId === 0) {
      this.loadItems()
      return
    }
    this.itemService.getItemsByCategory(this.chosenCategoryId).subscribe({
      next: (response) => {
        this.items = response
        console.log(response)
      }
    })
  }

  loadCategories() {
    this.itemService.getCategories().subscribe({
      next: (response) => {
        this.categories = response
        console.log(response)
      }
    })
  }
}

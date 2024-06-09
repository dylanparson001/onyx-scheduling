import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ItemsService} from "../_services/items.service";
import {Item} from "../models/item";
import {ItemCardComponent} from "./item-card/item-card.component";
import {FormsModule} from "@angular/forms";
import {Category} from "../models/category";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-items',
  standalone: true,
  imports: [CommonModule, ItemCardComponent, FormsModule, RouterLink],
  templateUrl: './items.component.html',
  styleUrl: './items.component.css'
})
export class ItemsComponent implements OnInit {
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

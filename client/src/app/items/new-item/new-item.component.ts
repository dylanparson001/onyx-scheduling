import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {CurrencyPipe, NgForOf, NgIf} from "@angular/common";
import {Item} from "../../models/item";
import {Category} from "../../models/category";
import {ItemsService} from "../../_services/items.service";
import {Router, RouterLink} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-new-item',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    RouterLink,
    NgIf,
    CurrencyPipe
  ],
  templateUrl: './new-item.component.html',
  styleUrl: './new-item.component.css'
})
export class NewItemComponent {
  newItem: Item = {
    id: 0,
    item_Name: "",
    category_id: 0,
    defaultQuantity: 0,
    price: 0,
    quantity: 0
  }
  categories: Category[] = [];

  constructor(
    private itemService: ItemsService,
    private router: Router,
    private toastr: ToastrService
  ) {}


  ngOnInit(): void {
    this.loadCategories()
  }
  amount: number = 0;

  // onInputChange(event: any): void {
  //   const input = event.target.value;
  //   // Remove all non-numeric characters (except for the decimal point)
  //   const cleanInput = input.replace(/[^0-9.]/g, '');
  //
  //   // Convert the cleaned input to a number
  //   this.newItem.price = parseFloat(cleanInput) || 0;
  //
  // }
  loadCategories() {
    this.itemService.getCategories().subscribe({
      next: (response) => {
        this.categories = response
      }
    })
  }

  addItem() {
    if (this.newItem.item_Name === '' ||
      this.newItem.category_id === 0 ||
      this.newItem.defaultQuantity <= 0) {
      this.toastr.error('Please fill all fields')
      return;
    }

    if (this.newItem){
    this.itemService.addItem(this.newItem).subscribe({
      next:() => {
        this.router.navigateByUrl('/items')
        this.toastr.info('Item added')
    }
    })
    }
  }
}

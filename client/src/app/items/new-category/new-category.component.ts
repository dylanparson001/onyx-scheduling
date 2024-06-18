import { Component } from '@angular/core';
import {CurrencyPipe, NgForOf, NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {ItemsService} from "../../_services/items.service";
import {ToastrService} from "ngx-toastr";
import {Category} from "../../models/category";

@Component({
  selector: 'app-new-category',
  standalone: true,
    imports: [
        CurrencyPipe,
        FormsModule,
        NgForOf,
        NgIf,
        RouterLink
    ],
  templateUrl: './new-category.component.html',
  styleUrl: './new-category.component.css'
})
export class NewCategoryComponent {
  companyId: string | null = localStorage.getItem('companyId')

  newCategory: Category = {
    companyId: "",
    id: 0,
    name: ""
  }
  constructor(
    private itemService: ItemsService,
    private router: Router,
    private toastr: ToastrService
  ) {
    if(this.companyId) {
      this.newCategory.companyId = this.companyId
    }
  }

  addCategory() {
    if (this.newCategory.name === '' || this.newCategory.name === null) {
      this.toastr.error('Please enter a name');
      return;
    }
      this.itemService.addCategory(this.newCategory).subscribe({
        next: () => {
          this.toastr.info('Category has been added')
          this.router.navigateByUrl('/items')
        },
        error: (err) => {
          this.toastr.error(err)
        }
      })
  }
}

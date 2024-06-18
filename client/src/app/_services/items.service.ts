import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Item} from "../models/item";
import {Observable, throwError} from "rxjs";
import {Category} from '../models/category';
import {catchError} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  baseUrl = environment.apiUrl;


  constructor(private http: HttpClient) {
  }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(
      `${this.baseUrl}InvoiceItem/GetInvoiceItems`
    );

  }

  getItemsByCategory(categoryId: number) {

    return this.http.get<Item[]>(
      `${this.baseUrl}InvoiceItem/GetInvoiceItemsByCategory?categoryId=${categoryId}`
    )
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(
      `${this.baseUrl}Category/GetAllCategories`
    )
  }

  addItem(item: Item) {
    return this.http.post<Item>(
      `${this.baseUrl}InvoiceItem/AddInvoiceItem`,
      item
    )
  }

  deleteItem(itemId: number) {
    return this.http.delete(
      `${this.baseUrl}InvoiceItem/DeleteInvoiceItem?itemId=${itemId}`
    )
  }

  deleteCategory(categoryId: number) {
    return this.http.delete(
      `${this.baseUrl}Category/DeleteCategory?categoryId=${categoryId}`
    )

  }

  addCategory(category: Category) {
    return this.http.post<Category>(
      `${this.baseUrl}Category/AddCategory`,
      category
    ).pipe(
      catchError(this.handleError)
    )
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error.message) {
      errorMessage = error.error.message;
    }
    return errorMessage;
  }

}

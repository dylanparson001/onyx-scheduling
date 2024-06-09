import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Item} from "../models/item";
import {Observable} from "rxjs";
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }
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



}

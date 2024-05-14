import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Item} from "../models/item";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }
  getItems(): Observable<Item[]> {
    let result =  this.http.get<Item[]>(
      `${this.baseUrl}InvoiceItem/GetInvoiceItems`
    );

    return result;
  }

  getCategories() {
    let result = this.http.get(
      `${this.baseUrl}Category/GetAllCategories`
    )
    return result
  }

}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Invoice } from '../models/invoice';
import { environment } from '../environments/environment';
import { Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoicesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllInvoices(): Observable<Invoice[]> {
      let result = this.http.get<Invoice[]>(this.baseUrl + "Invoices/GetAllInvoices")

      console.log(result);
      return result
  }

  getInvoiceItems(invoiceId: number) {
    return this.http.get(this.baseUrl + "InvoiceItem/GetInvoiceItemsFromInvoice/" + invoiceId)

  }

  getInvoicesByDate(date: string): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.baseUrl + `Invoices/GetInvoicesByDate?setDate=${date}`);
  }

}

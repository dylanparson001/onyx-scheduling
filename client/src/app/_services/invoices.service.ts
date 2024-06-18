import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Invoice } from '../models/invoice';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { ProcessingStatus } from '../enums/ProcessingStatus';
import {Item} from "../models/item";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root',
})
export class InvoicesService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  downloadPDF(invoiceId: number): Observable<void> {
    return this.http.get(
      `${this.baseUrl}Invoices/GetPdfFromInvoice?invoiceId=${invoiceId}`,
      { responseType: 'blob' }).pipe(
      map((blob: Blob) => {
        const downloadUrl = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = downloadUrl;
        a.download = `Invoice_${invoiceId}.pdf`; // You can set the file name here
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(downloadUrl);
      })
    );
  }
  getAllInvoices(): Observable<Invoice[]> {
    let result = this.http.get<Invoice[]>(
      `${this.baseUrl}Invoices/GetAllInvoices`
    );

    return result;
  }

  getInvoiceItems(invoiceId: number): Observable<Item[]> {
    return this.http.get<Item[]>(
      `${this.baseUrl}InvoiceItem/GetInvoiceItemsFromInvoice/${invoiceId}`
    );
  }

  getInvoicesByDate(date: string, status: string, position: number, take: number): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(
      `${this.baseUrl}Invoices/GetInvoicesByDate?setDate=${date}&status=${status}&position=${position}&take=${take}`
    );
  }
  getCountInvoicesByDate(date: string, status: string): Observable<number> {
    return this.http.get<number>(
      `${this.baseUrl}Invoices/GetCountOfInvoicesByDate?setDate=${date}&status=${status}`
    );
  }


  getProcessingStatusOptions(): Observable<ProcessingStatus[]> {
    return this.http.get<ProcessingStatus[]>(
      `${this.baseUrl}Invoices/GetProcessingStatuses`
    );
  }

  postInvoice(newInvoice: Invoice): Observable<Invoice> {
    return this.http.post<Invoice>(
      `${this.baseUrl}Invoices/AddInvoice`,
      newInvoice
    );
  }
}

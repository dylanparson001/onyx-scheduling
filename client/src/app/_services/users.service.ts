import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  baseUrl: string = environment.apiUrl;

  public user: User = {
    id: '',
    userName: '',
    firstName: '',
    lastName: '',
    address: '',
    city: '',
    state: '',
    phone: '',
    role: '',
    token: '',
  };
  roleAs: string = '';

  constructor(private http: HttpClient) {}

  // getAllUsers(): Observable<User> {
  //   return this.http.get<User>(this.baseUrl + "")
  // }

  getCustomersFromInvoiceId(invoiceId: string): Observable<User> {
    return this.http.get<User>(
      `${this.baseUrl}User/GetCustomerFromInvoice?customerId=${invoiceId}`
    );
  }

  getAllCustomers(): Observable<User[]> {
    let result = this.http.get<User[]>(`${this.baseUrl}User/GetAllCustomers`);
    return result;
  }

  getAllTechnicians(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}User/GetAllTechnicians`);
  }
}

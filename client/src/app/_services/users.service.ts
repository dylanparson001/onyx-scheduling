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
    Id: '',
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

  getCustomersFromInvoiceId(customerId: string): Observable<User> {
    return this.http.get<User>(
      `${this.baseUrl}User/GetCustomerFromInvoice?customerId=${customerId}`
    );
  }
  getTechniciansFromInvoiceId(technicianId: string): Observable<User> {
    console.log(technicianId)
    return this.http.get<User>(
      `${this.baseUrl}User/GetTechnicianFromInvoice?technicianId=${technicianId}`
    );
  }

  getAllCustomers(): Observable<User[]> {
    let result = this.http.get<User[]>(`${this.baseUrl}User/GetAllCustomers`);
    return result;
  }

  getAllTechnicians(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}User/GetAllTechnicians`);
  }

  updateUserInfo(userId: string, user: User) {
    return this.http.put(
      `${this.baseUrl}User/UpdateUserInfo?userId=${userId}`,
      user
    )
}
}

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

  constructor(private http: HttpClient) {}

  searchUsersByUsername(username: string): Observable<User[]> {
    return this.http.get<User[]>(
      `${this.baseUrl}User/SearchUsers?username=${username}`
    )
  }
  getCustomersFromInvoiceId(customerId: string): Observable<User> {
    return this.http.get<User>(
      `${this.baseUrl}User/GetCustomerFromInvoice?customerId=${customerId}`
    );
  }
  getTechniciansFromInvoiceId(technicianId: string): Observable<User> {
    return this.http.get<User>(
      `${this.baseUrl}User/GetTechnicianFromInvoice?technicianId=${technicianId}`
    );
  }

  getAllCustomers(): Observable<User[]> {
     return this.http.get<User[]>(`${this.baseUrl}User/GetAllCustomers`);
  }

  getAllTechnicians(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}User/GetAllTechnicians`);
  }

  getUsers( position: number, take: number): Observable<User[]> {
    return this.http.get<User[]>(
      `${this.baseUrl}User/GetUsers?position=${position}&take=${take}`
    )
  }
  getCountUsers() {
    return this.http.get<number>(
      `${this.baseUrl}User/GetCountOfUsers`
    )
  }

  updateUserInfo(userId: string, user: User) {
    return this.http.put(
      `${this.baseUrl}User/UpdateUserInfo?userId=${userId}`,
      user
    )
  }

  getUserFromUsername(username: string): Observable<User> {
    return this.http.get<User>(
      `${this.baseUrl}User/GetUserFromUsername?username=${username}`
    )
  }
}

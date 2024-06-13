import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../environments/environment';
import { loginDto } from '../dtos/loginDto';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {
  baseUrl: string = environment.apiUrl;

  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
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
    email: ''
  };
  roleAs: string = '';

  constructor (private http: HttpClient) { }

  login(model: loginDto){
    // http request to login api endpoint
    return this.http.post<any>(`${this.baseUrl}Authenticate/login`, model).pipe(
      map((response) => {
        const user = response;
        if (user){
          // TOKEN
          localStorage.setItem("user", JSON.stringify(user.token));
          localStorage.setItem('userName', user.userName)
          //USERID
          localStorage.setItem('userId', user.userId)

          // sets current staff member
          this.currentUserSource.next(user);
          this.user = user;
        }
      })
    )
  }

  register(model: any){
    return this.http.post<any>(this.baseUrl + 'Authenticate/register', model);
  }

  logout(){
    localStorage.removeItem("user");
    localStorage.removeItem('userName')
    localStorage.removeItem('userId')
    this.currentUserSource.next(null);
  }
  resetPassword(userId: string, currentPassword: string, newPassword: string) {
    let model = {
      userId : userId,
      currentPassword: currentPassword,
      newPassword: newPassword
    }
    return this.http.put(
      `${this.baseUrl}Authenticate/ResetPassword`,
      model
    )
  }
  forgotPassword(userId: string, newPassword: string) {
    let model = {
      userId : userId,
      newPassword: newPassword
    }
    return this.http.put(
      `${this.baseUrl}Authenticate/ForgotPassword`,
      model
    )
  }
  setCurrentUser(user: any){
    this.currentUserSource.next(user);

  }
  getRole() {
    return localStorage.getItem('role');
  }
  hasRole(role: string): boolean {
    const currentUser = this.currentUserSource.value;
    return currentUser ? currentUser.role === role : false;
  }

  getUserFromId( userId: string) {
    return this.http.get<User>(
      `${this.baseUrl}Authenticate/GetUserFromId?userId=${userId}`
    )
  }
}

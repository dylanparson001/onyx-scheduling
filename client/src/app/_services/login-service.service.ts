import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {BehaviorSubject, map, Observable} from 'rxjs';
import {User} from '../models/user';
import {environment} from '../environments/environment';
import {loginDto} from '../dtos/loginDto';
import {DATE_PIPE_DEFAULT_TIMEZONE} from "@angular/common";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {
  baseUrl: string = environment.apiUrl;
  private jwt: string = ''
  private expiryDate: any
  private expiryHours: number = 0
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  public user: User = {
    dailyTotal: 0,
    companyId: "",
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

  constructor(private http: HttpClient, private router: Router) {
  }

  login(model: loginDto) {
    // http request to login api endpoint
    return this.http.post<any>(`${this.baseUrl}Authenticate/login`, model).pipe(
      map((response) => {
        const user = response;
        if (user) {
          // TOKEN
          localStorage.setItem("user", JSON.stringify(user.token));
          localStorage.setItem('userName', user.userName)
          //USERID
          localStorage.setItem('userId', user.userId)
          localStorage.setItem('companyId', user.companyId)

          // sets current staff member
          this.currentUserSource.next(user);
          this.user = user;
          this.jwt = user.token
          this.expiryDate = this.getExpiryDateFromToken(this.jwt)
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<any>(this.baseUrl + 'Authenticate/register', model);
  }

  logout() {
    localStorage.removeItem("user");
    localStorage.removeItem('userName')
    localStorage.removeItem('userId')
    this.currentUserSource.next(null);
    if (this.expiryDate) clearInterval(this.expiryDate);
  }

  checkExpiry() {
    if (this.expiryDate && new Date() >= this.expiryDate) {
      this.logout();
      this.router.navigateByUrl("/login")
    }
  }

  getExpiryDateFromToken(token: string): Date | null {
    const tokenPayload = JSON.parse(atob(token.split('.')[1]));
    if (tokenPayload.exp) {
      // Token expiry time is in seconds since Unix epoch, convert to milliseconds
      return new Date(tokenPayload.exp * 1000);
    }
    return null;
  }

  resetPassword(userId: string, currentPassword: string, newPassword: string) {
    let model = {
      userId: userId,
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
      userId: userId,
      newPassword: newPassword
    }
    return this.http.put(
      `${this.baseUrl}Authenticate/ForgotPassword`,
      model
    )
  }

  setCurrentUser(user: any) {
    this.currentUserSource.next(user);
  }

  getRole() {
    return localStorage.getItem('role');
  }

  hasRole(role: string): boolean {
    const currentUser = this.currentUserSource.value;
    return currentUser ? currentUser.role === role : false;
  }

  getUserFromId(userId: string) {
    return this.http.get<User>(
      `${this.baseUrl}Authenticate/GetUserFromId?userId=${userId}`
    )
  }

  getCurrentUser() {
    const userId = localStorage.getItem('userId')
    if (userId) {
      return this.http.get<User>(
        `${this.baseUrl}Authenticate/GetUserFromId?userId=${userId}`
      )
    }

    return this.http.get<User>(
      `${this.baseUrl}Authenticate/GetUserFromId?userId=0`
    )
  }
}

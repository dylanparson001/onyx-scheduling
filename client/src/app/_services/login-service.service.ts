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
    id: '',
    userName: '',
    firstName: '',
    lastName: '',
    address: '',
    city: '',
    state: '',
    phone: '',
    role: '',
    token: ''
  };
  roleAs: string = '';

  constructor (private http: HttpClient) { }

  login(model: loginDto){
    // http request to login api endpoint
    return this.http.post<User>(this.baseUrl + "Authenticate/login", model).pipe(
      map((response) => {
        const user = response;
        if (user){

          // puts staff into local storage
          localStorage.setItem("user", JSON.stringify(user.token));
          localStorage.setItem('role', JSON.stringify(user.role))
          localStorage.setItem('userName', user.userName)
          // sets current staff member
          this.currentUserSource.next(user);
          this.user = user;
        }
      })
    )
  }

  register(model: any){
    return this.http.post<any>(this.baseUrl + 'Authenticate/register', model).pipe(
      map(user  => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user.token));
          localStorage.setItem('role', JSON.stringify(user.role))
          this.currentUserSource.next(user)

        }
      })
    )
  }
  logout(){
    localStorage.removeItem("user");
    localStorage.removeItem("role");
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: any){
    this.currentUserSource.next(user);

  }
  getRole() {
      return localStorage.getItem('role');
  }
}

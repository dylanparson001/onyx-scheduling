import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {
  baseUrl: string = "http://localhost:5000/api/"

  private currentUserSource = new BehaviorSubject<any | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  public user: any = '';

  constructor (private http: HttpClient) { }

  login(model: any){
    // http request to login api endpoint
    return this.http.post<User>(this.baseUrl + "Authenticate/login", model).pipe(
      map((response) => {
        const user = response;
        if (user){
          // puts staff into local storage
          localStorage.setItem("user", JSON.stringify(user));
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
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user)

        }
      })
    )
  }
  logout(){
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: any){
    this.currentUserSource.next(user);

  }
}

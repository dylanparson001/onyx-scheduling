// navigation.service.ts
import {Injectable} from '@angular/core';
import {NAV_ITEMS} from "../navbar/navigation.config";
import {LoginServiceService} from "./login-service.service";
import {User} from "../models/user";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  constructor(private authService: LoginServiceService) {
  }

  getNavItems(): Observable<any[]> {
    return this.authService.currentUser$.pipe(
      map(user => {
        if (user) {
          return NAV_ITEMS.filter(item => item.roles.includes(user.role));
        }
        return [];
      })
    );
  }
}

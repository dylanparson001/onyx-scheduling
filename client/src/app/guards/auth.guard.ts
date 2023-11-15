import { CanActivateFn } from '@angular/router';
import { LoginServiceService } from '../_services/login-service.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authService: LoginServiceService = inject (LoginServiceService)

  const currentUser = localStorage.getItem('user');

  if (currentUser) {
    return true;
  }
  return false;
  // return authService.currentUser$.pipe(
  //   // pipe and map allow us to perform actions with the variable
  //   map((user) => {
  //     console.log(user)
  //     // If the employee is signed in, they can access the routes, if they are not, they cannot access the routes
  //     if (user) return true;
  //     else {
  //       return false;
  //     }
  //   })
  // )
};

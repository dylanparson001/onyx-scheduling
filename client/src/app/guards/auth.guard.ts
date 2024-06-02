import { CanActivateFn } from '@angular/router';
import { LoginServiceService } from '../_services/login-service.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';


export const authGuard: CanActivateFn = (route, state) => {
  const authService: LoginServiceService = inject (LoginServiceService)

  const currentUser = authService.currentUser$

  if (currentUser) {
    return true;
  }
  return false;

};

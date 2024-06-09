import { CanActivateFn, Router } from '@angular/router';
import { LoginServiceService } from '../_services/login-service.service';
import { inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../models/user';
import {ToastrService} from "ngx-toastr";
import {error} from "@angular/compiler-cli/src/transformers/util";

export const authGuard: CanActivateFn = (route, state) => {
  const authService: LoginServiceService = inject(LoginServiceService);
  const router: Router = inject(Router);
  const userId: string | null = localStorage.getItem('userId');
  if (!userId) {
    router.navigate(['/login']);
    return of(false);
  }

  return authService.getUserFromId(userId).pipe(
    map((user: User) => {
      const requiredRoles = route.data['roles'] as string[];
      if (requiredRoles && !requiredRoles.some(role => user.role.includes(role))) {
        router.navigate(['/login']);
        return false;
      }
      return true;
    }),
    catchError((error) => {
      if (error.status === 401) {
        router.navigate(['/login']);
      } else {
      }
      return of(false);
    })
  );
};

import {HttpInterceptorFn} from '@angular/common/http';
import {LoginServiceService} from "../_services/login-service.service";
import {inject} from "@angular/core";
import {Router, RouterModule} from "@angular/router";

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  let loginService = inject(LoginServiceService)
  let router = inject(Router)

  let jwt: string | null = localStorage.getItem('user')
  if (jwt) {
    loginService.checkExpiry()

    jwt = jwt.replace(/['"]+/g, '');

    req = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${jwt}`)
    })
  }
  return next(req);
};

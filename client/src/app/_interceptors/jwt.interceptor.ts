import {HttpInterceptorFn} from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  let jwt: string | null = localStorage.getItem('user')
  if (jwt) {
    req = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${jwt}`)
    })
  }
  return next(req);
};

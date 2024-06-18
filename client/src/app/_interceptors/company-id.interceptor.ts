import { HttpInterceptorFn } from '@angular/common/http';

export const companyIdInterceptor: HttpInterceptorFn = (req, next) => {
  let companyId: string | null = localStorage.getItem('companyId')
  if (companyId) {
    req = req.clone({
      headers: req.headers.set('CompanyId', `${companyId}`)
    })
  }
  return next(req);
};

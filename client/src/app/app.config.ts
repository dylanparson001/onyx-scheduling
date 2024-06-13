import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {HttpClientModule, provideHttpClient, withInterceptors} from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import {jwtInterceptor} from "./_interceptors/jwt.interceptor";
import {provideToastr, ToastrModule} from "ngx-toastr";
import {PhoneNumberPipe} from "./phone-number.pipe";

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideToastr({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      jwtInterceptor
    ])),
]
};

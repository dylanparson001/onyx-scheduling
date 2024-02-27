import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {HttpClientModule, provideHttpClient, withInterceptors} from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import {jwtInterceptor} from "./_interceptors/jwt.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      jwtInterceptor
    ])),
    provideAnimations()
]
};

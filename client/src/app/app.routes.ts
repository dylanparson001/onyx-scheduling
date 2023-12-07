import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { authGuard } from './guards/auth.guard';
import { InvoiceComponent } from './invoice/invoice.component';

export const routes: Routes = [
  { path: 'login', component: LoginFormComponent },
  { path: 'home', component: HomeComponent, canActivate: [authGuard]},
  { path: 'invoices', component: InvoiceComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '/login' }, // Default route
  { path: '', redirectTo: '/home', pathMatch: 'full' }

];

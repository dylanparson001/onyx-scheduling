import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { authGuard } from './guards/auth.guard';
import { InvoiceComponent } from './invoice/invoice.component';
import { NewInvoiceFormComponent } from './invoice/new-invoice-form/new-invoice-form.component';
import { EditInvoiceFormComponent } from './invoice/edit-invoice-form/edit-invoice-form.component';
import {JobsComponent} from "./jobs/jobs.component";
import {NewJobFormComponent} from "./jobs/new-job-form/new-job-form.component";

export const routes: Routes = [
  { path: 'login', component: LoginFormComponent },
  { path: 'home', component: HomeComponent, canActivate: [authGuard]},
  { path: 'invoices', component: InvoiceComponent, canActivate: [authGuard] },
  { path: 'invoices/new-invoice', component: NewInvoiceFormComponent, canActivate: [authGuard] },
  { path: 'invoices/edit-invoice', component: EditInvoiceFormComponent, canActivate: [authGuard] },
  { path: 'jobs', component: JobsComponent, canActivate: [authGuard] },
  { path: 'jobs/new-job', component: NewJobFormComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '/login' }, // Default route
  { path: '', redirectTo: '/home', pathMatch: 'full' }

];

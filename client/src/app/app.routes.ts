// app-routing.module.ts
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './account/login-form/login-form.component';
import { authGuard } from './guards/auth.guard';
import { InvoiceComponent } from './invoice/invoice.component';
import { NewInvoiceFormComponent } from './invoice/new-invoice-form/new-invoice-form.component';
import { EditInvoiceFormComponent } from './invoice/edit-invoice-form/edit-invoice-form.component';
import { JobsComponent } from './jobs/jobs.component';
import { NewJobFormComponent } from './jobs/new-job-form/new-job-form.component';
import { EditJobComponent } from './jobs/edit-job/edit-job.component';
import { ItemsComponent } from './items/items.component';
import { AccountComponent } from './account/account.component';
import {ForgotPasswordComponent} from "./account/forgot-password/forgot-password.component";
import {RegisterComponent} from "./account/register/register.component";
import {ManageAccountsComponent} from "./account/manage-accounts/manage-accounts.component";
import {ManageUsersComponent} from "./account/manage-accounts/manage-users/manage-users.component";
import {NewItemComponent} from "./items/new-item/new-item.component";

export const routes: Routes = [
  { path: 'login', component: LoginFormComponent },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },
  { path: 'account', component: AccountComponent, canActivate: [authGuard] },
  { path: 'account/forgot-password', component: ForgotPasswordComponent},
  { path: 'account/register', component: RegisterComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'account/manage-accounts', component: ManageAccountsComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'account/manage-accounts/manage', component: ManageUsersComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'invoices', component: InvoiceComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'invoices/new-invoice', component: NewInvoiceFormComponent, canActivate: [authGuard] },
  { path: 'invoices/edit-invoice', component: EditInvoiceFormComponent, canActivate: [authGuard] },
  { path: 'jobs', component: JobsComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'jobs/new-job', component: NewJobFormComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'jobs/edit-job', component: EditJobComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'items', component: ItemsComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: 'items/new-item', component: NewItemComponent, canActivate: [authGuard], data: { roles: ['Office', 'Admin'] } },
  { path: '**', redirectTo: '/login' }, // Default route
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

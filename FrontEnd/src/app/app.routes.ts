import { Routes } from '@angular/router';
import { UserListComponent } from './components/user-list/user-list.component';
import { CreateUserComponent } from './components/create-user/create-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';

export const routes: Routes = [
  { path: 'users', component: UserListComponent },
  { path: 'create-user', component: CreateUserComponent },
  { path: 'edit-user/:id', component: EditUserComponent },
  { path: '', redirectTo: '/users', pathMatch: 'full' },
];

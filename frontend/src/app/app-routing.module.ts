import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlbumsComponent } from './albums/albums.component';
import { UsersComponent } from './users/users.component';
import { SongsComponent } from './songs/songs.component';
import { StoresComponent } from './stores/stores.component';
import { LoginComponent } from './login/login.component';
import { CartComponent } from './cart/cart.component';
import { RegisterComponent } from './register/register.component';
import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard.component';


const routes: Routes = [
  {path:'', redirectTo:'/login', pathMatch:'full'},
  {path:'albums', component: AlbumsComponent},
  {path: 'stores', component:StoresComponent},
  {path: 'songs', component:SongsComponent},
  {path: 'login', component:LoginComponent},
  {path:'cart', component: CartComponent},
  {path: 'register', component:RegisterComponent},
  {path: 'employee', component:EmployeeDashboardComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

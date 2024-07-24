import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlbumsComponent } from './albums/albums.component';
import { UsersComponent } from './users/users.component';
import { SongsComponent } from './songs/songs.component';
import { StoresComponent } from './stores/stores.component';
import { LoginComponent } from './login/login.component';
import { AddAlbumComponent } from './albums/add-album/add-album.component';
import { CartComponent } from './cart/cart.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path:'', redirectTo:'/login', pathMatch:'full'},
  {path:'albums', component: AlbumsComponent},
  {path: 'stores', component:StoresComponent},
  {path: 'songs', component:SongsComponent},
  {path: 'login', component:LoginComponent},
  {path:'add-album', component:AddAlbumComponent},
  {path:'cart', component: CartComponent},
  {path: 'register', component:RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

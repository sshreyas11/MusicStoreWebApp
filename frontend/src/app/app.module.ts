import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AlbumsComponent } from './albums/albums.component';
import { UsersComponent } from './users/users.component';
import { SongsComponent } from './songs/songs.component';
import { StoresComponent } from './stores/stores.component';
import { CartComponent } from './cart/cart.component';
import { RouterModule } from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import { AddAlbumComponent } from './albums/add-album/add-album.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { DialogueComponent } from './dialogue/dialogue.component';


@NgModule({
  declarations: [
    AppComponent,
    AlbumsComponent,
    UsersComponent,
    SongsComponent,
    StoresComponent,
    CartComponent,
    AddAlbumComponent,
    RegisterComponent,
    LoginComponent,
    DialogueComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

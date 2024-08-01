import { Component } from '@angular/core';
import { Album } from '../models/album.model';
import { AlbumService } from '../services/album/album.service';
import { CartService } from '../services/cart/cart.service';
import { LoginService } from '../services/login/login.service';
import { DialogueComponent } from '../dialogue/dialogue.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styleUrls: ['./albums.component.scss']
})
export class AlbumsComponent {
  albumCollection: Album[] = [];
  loading: boolean = false;
  showNotification: boolean = false;

  constructor(
    private album_service: AlbumService, 
    private cartService: CartService, 
    public loginService:LoginService, 
    public dialog: MatDialog){}

  ngOnInit():void{
    this.album_service.getAlbums().subscribe((album_data: Album[])=>{
      this.albumCollection = album_data;
      setTimeout(() => {
        this.loading = false;
      }, 3000);
    }, error => {
      this.loading = true;
    });
  }

  addToCart(album: Album) {
    this.cartService.addToCart(album);
    this.showAddToCartDialog(album);
  }

  showAddToCartDialog(album: Album): void {
    this.dialog.open(DialogueComponent, {
      data: { album }
    });
  }

  
}

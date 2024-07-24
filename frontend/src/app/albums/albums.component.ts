import { Component } from '@angular/core';
import { Album } from '../models/album.model';
import { AlbumService } from '../services/album/album.service';
import { CartService } from '../services/cart/cart.service';
import {RouterModule} from '@angular/router';


@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styleUrls: ['./albums.component.scss']
})
export class AlbumsComponent {
  albumCollection: Album[] = [];
  cart_count: number = 0;
  constructor(private album_service: AlbumService, private cartService: CartService){

  }
  ngOnInit():void{
    this.album_service.getAlbums().subscribe((album_data: Album[])=>{
      this.albumCollection = album_data;
    });

  }

  addToCart(album: Album) {
    this.cart_count+=1;
    this.cartService.addToCart(album);
  }
}

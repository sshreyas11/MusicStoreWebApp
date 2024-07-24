import { Injectable } from '@angular/core';
import { Album } from 'src/app/models/album.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor() { }
  cart: Album[] = [];

  addToCart(album: Album) {
    this.cart.push(album);
  }

  getCart() {
    return this.cart;
  }
}

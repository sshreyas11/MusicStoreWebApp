import { Injectable } from '@angular/core';
import { Album } from 'src/app/models/album.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor() { }
  private cart: { album: Album, quantity: number }[] = [];

  addToCart(album: Album): void {
    const existingItem = this.cart.find(item => item.album.album_name === album.album_name);
    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      this.cart.push({ album, quantity: 1 });
    }
  }

  getCart(): { album: Album, quantity: number }[] {
    return this.cart;
  }
}

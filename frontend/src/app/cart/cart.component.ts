import { Component } from '@angular/core';
import { CartService } from '../services/cart/cart.service';
import { Album } from '../models/album.model';
import { Router } from '@angular/router';
import { NavbarService } from '../services/navbar/navbar.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  cart: { album: Album, quantity: number }[] = [];
  totalItems: number = 0;
  totalAmount: number = 0;

  constructor(private cartService: CartService, private router: Router, private navbarService: NavbarService){}

  ngOnInit() {
    this.cart = this.cartService.getCart();
    this.calculateTotals();
  }

  calculateTotals(): void {
    this.totalItems = this.cart.reduce((acc, item) => acc + item.quantity, 0);
    this.totalAmount = this.cart.reduce((acc, item) => acc + (item.album.album_price * item.quantity), 0);
  }

  goToAlbums(): void {
    this.router.navigate(['/albums']);
  }

}

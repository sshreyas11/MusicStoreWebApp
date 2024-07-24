import { Component } from '@angular/core';
import { CartService } from '../services/cart/cart.service';
import { Album } from '../models/album.model';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  cart: Album[] = [];
  constructor(private cartService: CartService){}

  ngOnInit() {
    this.cart = this.cartService.getCart();
  }

}

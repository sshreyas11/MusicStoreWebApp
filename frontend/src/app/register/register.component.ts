import { Component } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import * as CryptoJS from 'crypto-js';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  user: User = new User();
  user_types: string[] = ['Employee', 'Customer'];
  error_message: string = '';
 
  constructor(private userService:UserService, private router:Router){}
 

  onSubmit(){
    console.log(this.user);
    // const hashedPassword = CryptoJS.SHA256(this.user.password_hash).toString();
    // this.user.password_hash = hashedPassword;
    this.userService.register(this.user).subscribe(
      response => {
        console.log('Registration successful', response);
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Registration failed', error);
        this.error_message = 'Registration failed. Please try again.';
      }
    );
  }
}

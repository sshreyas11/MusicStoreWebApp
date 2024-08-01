import { Component } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { LoginService } from '../services/login/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  user: User = new User();
  user_types: string[] = ['Employee', 'Customer'];
  error_message: string = '';
 
  constructor(private userService:UserService, private router:Router, public loginService:LoginService){}
 

  onSubmit(){
    console.log(this.user);
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
  
  get_user(){
    return this.loginService.usr_name;
  }
}

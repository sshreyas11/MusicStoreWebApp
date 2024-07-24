import { Component, ViewEncapsulation } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { AlbumsComponent } from '../albums/albums.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class LoginComponent {
  error_message: string = '';
  login: Login = new Login();

  constructor(public login_service:LoginService, private router: Router){}

  onSubmit():void{
    this.login_service.login(
      this.login.email,
      this.login.password
    ).subscribe(
      success=>{
        if(success){
          console.log("Login Successful");
          this.router.navigate(['/albums']);
        }else{
          this.error_message = 'Invalid Login';
        }
      }
    );
  }
}

class Login{
  email: string;
  password:string;
  constructor(){
    this.email = '';
    this.password = '';
  }
}

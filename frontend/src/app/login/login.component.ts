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
  email: string = '';
  password: string = '';
  error_message: string = '';

  constructor(public login_service:LoginService, private router: Router){}
  onSubmit(){
    this.login_service.login(this.email, this.password).subscribe(
      respons =>{
        if(respons){
          this.router.navigate(['/albums']);
        }else{
          this.error_message = 'Invalid Login';
        }

      },
      error =>{
        this.error_message ='An error has occurred';
      }
    );
  }
}

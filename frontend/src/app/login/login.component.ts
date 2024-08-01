import { Component, ViewEncapsulation } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { AlbumsComponent } from '../albums/albums.component';
import { Router } from '@angular/router';
import { NavbarService } from '../services/navbar/navbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent {
  loading: boolean = false;
  email: string = '';
  password: string = '';
  error_message: string = '';

  constructor(public login_service: LoginService, private router: Router, private navbarService: NavbarService) {}

  ngOnInit(): void {
    this.navbarService.hide();
  }


  onSubmit() {
    this.login_service.login(this.email, this.password).subscribe(
      (response) => {
        if (response) {
          localStorage.setItem('userName', response.first_name); 
          const role: number = response.userType;
          
          if (role === 0) {
            localStorage.setItem('userType', "emp");
            this.router.navigate(['/employee']);
          } else {
            localStorage.setItem('userType', "cust");
            this.router.navigate(['/albums']);
          }
          this.loading = false;
        } else {
          this.loading = false;
          this.error_message = 'Invalid Login';
        }
      },
      (error) => {
        this.loading = false;

        this.error_message = 'An error has occurred';
      }
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { NavbarService } from '../services/navbar/navbar.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  userName: string | null = '';
  isVisible: boolean = true;
  userType: string | null = '';

  constructor(private navbarService: NavbarService, public loginService:LoginService, public router:Router) { }

  ngOnInit(): void {
    this.userName = localStorage.getItem("userName");
    this.userType = localStorage.getItem('userType');
    this.navbarService.visible$.subscribe(visible => {
      this.isVisible = visible;
    });
  }

  logout(): void {
    this.loginService.logout();
    this.navbarService.hide();
    this.router.navigate(['/login']);
  }
}

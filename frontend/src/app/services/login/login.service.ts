import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';


export const AUTH_TOKEN_KEY = 'auth-token';
export const usr_data = 'usr-name';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public authToken: string | null = null;
  public usr_name: string | null = null;
  private apiURL = 'http://localhost:5283/api/User/Login';
  constructor(private http: HttpClient) {
    this.checkStorage;
  }

  checkStorage() {
    const authToken = sessionStorage.getItem(AUTH_TOKEN_KEY);
    const firstName = sessionStorage.getItem(usr_data);
    this.authToken = authToken;
    if (firstName) {
      this.usr_name = firstName;
    } else {
      this.usr_name = null;
    }
  }

  login(email: string, password: string): Observable<any> {
    sessionStorage.setItem(AUTH_TOKEN_KEY, email);
    this.checkStorage();
    return this.http
      .post<any>(this.apiURL, { cust_email: email, cust_password: password })
      .pipe(
        tap((response) => {
          const userObj = response;
          if (userObj) {
            sessionStorage.setItem(usr_data, JSON.stringify(userObj));
          }
          console.log(sessionStorage[usr_data]);
        })
      );
  }

  public isLoggedIn(){
    return this.authToken!==null;
  }

  public logout(){
    if(!this.isLoggedIn()) return;
    sessionStorage.clear();
    this.checkStorage();

  }
}

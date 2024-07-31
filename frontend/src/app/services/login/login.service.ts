import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiURL = 'http://localhost:5283/api/User/Login';
  constructor(private http: HttpClient) {}

   login(username:string, password:string): Observable<any>{
    return this.http.post<any>(this.apiURL, {cust_email:username, cust_password:password});
   }
}

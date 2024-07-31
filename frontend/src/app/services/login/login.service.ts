import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiURL = '';
  constructor(private http: HttpClient) {

   }

   login(username:string, password:string): Observable<any>{
    return this.http.post<any>('${this.apiURL}/login', {cust_email:username, cust_password:password});
   }
}

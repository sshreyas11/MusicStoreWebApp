import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  sample_usrname = 'username';
  sample_password = 'password';
  constructor() {

   }

   login(username:string, password:string): Observable<boolean>{
    if(username===this.sample_usrname && password===this.sample_password){
      return of(true);
    }else{
      return of(false);
    }
   }
}

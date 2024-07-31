import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  private register_user_endpoint = 'http://localhost:5283/api/User/Register';
  

  constructor(private http: HttpClient) { }

  register(user: User): Observable<any> {
    console.log(user);
    return this.http.post(this.register_user_endpoint, user);
  }
}

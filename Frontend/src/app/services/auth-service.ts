import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { LoginDTO, RegisterDTO } from '../models/auth.model';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl + 'Auth/';

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {}

  login(model: LoginDTO): Observable<any> {
    return this.http.post(this.baseUrl + 'Login', model);
  }

  register(model: RegisterDTO): Observable<any> {
    return this.http.post(this.baseUrl + 'Register', model);
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}

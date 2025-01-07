import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { UserDto } from '../models/userDto';
import { RegistrationResponseDto } from '../models/response/registrationResponseDto';
import { AuthResponseDto } from '../models/response/authenticationResponseDto';
import { User } from '../models/User';
import { Observable, tap } from 'rxjs';
import { UserService } from './user.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService,
    private userService: UserService,
    private router: Router
  ) {}
  user = new User();
  public registerUser = (route: string, body: UserDto) => {
    return this.http.post(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body,
      { responseType: 'text' }
    );
  };
  public loginUser = (route: string, body: UserDto) => {
    return this.http.post<AuthResponseDto>(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body
    );
  };
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  };
  public saveData(user: User) {
    this.user = user;
  }
  public logoutUser() {
    localStorage.clear();
    alert('Your session expired!');
    this.router.navigate(['/login-main']);
  }
  public loggedIn() {
    return !!localStorage.getItem('token');
  }
  // getUserData(): User {
  //   let user = new User();
  //   let token = localStorage.getItem('token') || '';
  //   user = JSON.parse(token);
  //   return user;
  // }
}

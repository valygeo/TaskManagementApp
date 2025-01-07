import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { User } from '../models/User';
import { Observable, Subject, tap } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(public userService: ApiService) {}
  public getAllUsers(): Observable<User[]> {
    return this.userService.get('allUsers');
  }

  public getUserById(userId: number): Observable<User> {
    return this.userService.getById(userId);
  }
  public getUsersByProjectId(projectId: number): Observable<User[]> {
    return this.userService.getUsers(projectId);
  }
  public getUserByUsername(username: string): Observable<User> {
    return this.userService.getByUsername(username);
  }

  private _refreshRequired = new Subject<void>();
  get refreshRequired() {
    return this._refreshRequired;
  }
  public enrollUser(userId: number, projectId: number): Observable<any> {
    // return this.userService.enrollUser(userId, projectId).pipe(
    //   tap(() => {
    //     this._refreshRequired.next();
    //   })
    // );
    return this.userService.enrollUser(userId, projectId);
  }
  public disenrollUser(userId: number, projectId: number): Observable<any> {
    return this.userService.disenrollUser(userId, projectId);
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/User';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  private urlGetAllUsers = 'https://localhost:7098/api/User/GetAllUsers';
  private urlGetUserById = 'https://localhost:7098/api/User/GetUserById/';
  private urlGetUsersByProjectId =
    'https://localhost:7098/api/User/UsersByProject/';
  private urlGetUserByUsername =
    'https://localhost:7098/api/User/GetUserByUsername/';
  private urlEnrollUser = 'https://localhost:7098/api/User/EnrollUser/';
  private urlDisenrollUser =
    'https://localhost:7098/api/User/DeleteAssignment/';
  // get(url: string, Params?: Params): Observable<any> {
  //   let token = localStorage.getItem('token') || '';
  //   return this.http.get<User>(this._url, {
  //     headers: new HttpHeaders({
  //       Authorization: ' bearer ' + token,
  //     }),
  //   });
  // }
  public get(url: string, Params?: Params): Observable<User[]> {
    return this.http.get<User[]>(this.urlGetAllUsers);
  }
  public getUsers(projectId: number): Observable<User[]> {
    return this.http.get<User[]>(this.urlGetUsersByProjectId + projectId);
  }
  public getById(userId: number): Observable<User> {
    return this.http.get<User>(this.urlGetUserById + userId);
  }
  public getByUsername(username: string): Observable<User> {
    return this.http.get<User>(this.urlGetUserByUsername + username);
  }
  // public enrollUser(userId: number, projectId: number): Observable<any> {
  //   return this.http.post(this.urlEnrollUser, {
  //     params: { userId, projectId },
  //   });
  // }
  public enrollUser(userId: number, projectId: number): Observable<any> {
    return this.http.post(this.urlEnrollUser + userId + '/' + projectId, {
      responseType: 'text',
    });
  }
  public disenrollUser(userId: number, projectId: number): Observable<any> {
    return this.http.delete(this.urlDisenrollUser + userId + '/' + projectId, {
      responseType: 'text',
    });
  }
  // getUserData(): Observable<User> {
  //   let token = localStorage.getItem('token') || '';
  //   return this.http.get<User>(this._url2, {
  //     headers: new HttpHeaders({
  //       Authorization: ' bearer ' + token,
  //     }),
  //   });
  // }
}

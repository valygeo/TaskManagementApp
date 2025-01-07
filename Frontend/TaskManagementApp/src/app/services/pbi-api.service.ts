import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { Observable } from 'rxjs';
import { Pbi } from '../models/Pbi';
import { PbiDto } from '../models/PbiDto';

@Injectable({
  providedIn: 'root',
})
export class PbiApiService {
  constructor(private http: HttpClient) {}
  private urlGetAllPbi = 'https://localhost:7246/api/Pbi/GetAllPbi';
  private urlGetPbiByUserIdAndSprintId =
    'https://localhost:7246/api/Pbi/GetPbiByUserIdAndSprintId/';
  private urlAddPbi = 'https://localhost:7246/api/Pbi/addPbi/';
  private urlUpdatePbi = 'https://localhost:7246/api/Pbi/UpdatePbi/';
  private urlDeleteById = 'https://localhost:7246/api/Pbi/DeletePbi/';
  private urlGetById = 'https://localhost:7246/api/Pbi/GetPbiById/';
  public get(url: string, Params?: Params): Observable<Pbi[]> {
    return this.http.get<Pbi[]>(this.urlGetAllPbi);
  }
  public getPbiByUserIdSprintId(
    userId: number,
    sprintId: number
  ): Observable<Pbi[]> {
    return this.http.get<Pbi[]>(
      this.urlGetPbiByUserIdAndSprintId + userId + '/' + sprintId
    );
  }
  public addPbi(pbi: PbiDto): Observable<any> {
    return this.http.post(this.urlAddPbi, pbi, {
      responseType: 'text',
    });
  }
  public updatePbi(pbiId: number, pbi: PbiDto): Observable<any> {
    return this.http.put(this.urlUpdatePbi + pbiId, pbi, {
      responseType: 'text',
    });
  }
  public deleteById(pbiId: number): Observable<any> {
    return this.http.delete(this.urlDeleteById + pbiId, {
      responseType: 'text',
    });
  }
  public getById(pbiId: number): Observable<Pbi> {
    return this.http.get<Pbi>(this.urlGetById + pbiId);
  }
}

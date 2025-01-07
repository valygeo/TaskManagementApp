import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { Observable } from 'rxjs';
import { Sprint } from '../models/Sprint';
import { SprintDto } from '../models/SprintDto';

@Injectable({
  providedIn: 'root',
})
export class SprintApiService {
  constructor(private http: HttpClient) {}
  private urlGetAll = 'https://localhost:7290/api/Sprint/GetAllSprints';
  private urlGetById = 'https://localhost:7290/api/Sprint/GetSprintById/';
  private urlGetByName = 'https://localhost:7290/api/Sprint/GetSprintByName/';
  private urlDeleteById = 'https://localhost:7290/api/Sprint/DeleteSprint/';
  private urlGetSprintByProjectId =
    'https://localhost:7290/api/Sprint/GetByProjectId/';
  private urlDeleteByName =
    'https://localhost:7290/api/Sprint/DeleteSprintByName/';
  private urlAddSprint = 'https://localhost:7290/api/Sprint/AddSprint/';

  public get(url: string, Params?: Params): Observable<Sprint[]> {
    return this.http.get<Sprint[]>(this.urlGetAll);
  }
  public getById(sprintId: number): Observable<Sprint> {
    return this.http.get<Sprint>(this.urlGetById + sprintId);
  }
  public getByName(sprintName: string): Observable<Sprint> {
    return this.http.get<Sprint>(this.urlGetByName + sprintName);
  }
  public deleteById(sprintId: number): Observable<any> {
    return this.http.delete(this.urlDeleteById + sprintId, {
      responseType: 'text',
    });
  }
  public deleteByName(sprintName: string): Observable<any> {
    return this.http.delete(this.urlDeleteByName + sprintName, {
      responseType: 'text',
    });
  }
  public addSprint(sprint: SprintDto): Observable<any> {
    return this.http.post(this.urlAddSprint, sprint, {
      responseType: 'text',
    });
  }
  public getByProjectId(projectId: number): Observable<Sprint[]> {
    return this.http.get<Sprint[]>(this.urlGetSprintByProjectId + projectId);
  }
}

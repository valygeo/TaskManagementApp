import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { Observable, Subject, tap } from 'rxjs';
import { Project } from '../models/Project';
import { ProjectDto } from '../models/ProjectDto';

@Injectable({
  providedIn: 'root',
})
export class ProjectApiService {
  constructor(private http: HttpClient) {}

  private urlGetAll = 'https://localhost:7031/api/Project/GetAllProjects/';
  private urlGetById = 'https://localhost:7031/api/Project/GetProjectById/';
  private urlGetByName = 'https://localhost:7031/api/Project/GetProjectByName/';
  private urlDeleteById = 'https://localhost:7031/api/Project/DeleteProject/';
  private urlDeleteByName =
    'https://localhost:7031/api/Project/DeleteProjectByName/';
  private urlAddProject = 'https://localhost:7031/api/Project/AddProject/';
  private urlUpdateProject =
    'https://localhost:7031/api/Project/UpdateProject/';
  private urlAddProjectWithAssignment =
    'https://localhost:7031/api/Project/AddProjectWithAssignment/';

  public get(url: string, Params?: Params): Observable<Project[]> {
    return this.http.get<Project[]>(this.urlGetAll);
  }

  public getById(projectId: number): Observable<Project> {
    return this.http.get<Project>(this.urlGetById + projectId);
  }
  public getByName(projectName: string): Observable<Project> {
    return this.http.get<Project>(this.urlGetByName + projectName);
  }
  public deleteById(projectId: number): Observable<any> {
    return this.http.delete(this.urlDeleteById + projectId, {
      responseType: 'text',
    });
  }
  public deleteByName(projectName: string): Observable<any> {
    return this.http.delete(this.urlDeleteByName + projectName, {
      responseType: 'text',
    });
  }
  public addProject(project: ProjectDto): Observable<any> {
    return this.http.post(this.urlAddProject, project, {
      responseType: 'text',
    });
  }
  public addProjectWithAssignment(
    userId: number,
    project: ProjectDto
  ): Observable<any> {
    return this.http.post(this.urlAddProjectWithAssignment + userId, project, {
      responseType: 'text',
    });
  }
  public updateProject(
    projectId: number,
    project: ProjectDto
  ): Observable<any> {
    return this.http.put(this.urlUpdateProject + projectId, project, {
      responseType: 'text',
    });
  }
}

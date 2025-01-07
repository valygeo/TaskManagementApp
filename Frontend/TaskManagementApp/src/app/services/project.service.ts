import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';
import { Project } from '../models/Project';
import { ProjectDto } from '../models/ProjectDto';
import { ProjectApiService } from './project-api.service';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  constructor(public projectService: ProjectApiService) {}
  private _refreshRequired = new Subject<void>();
  get refreshRequired() {
    return this._refreshRequired;
  }
  public getAllProjects(): Observable<Project[]> {
    return this.projectService.get('allProjects');
  }
  public getProjectById(projectId: number): Observable<Project> {
    return this.projectService.getById(projectId);
  }
  public getProjectByName(projectName: string): Observable<Project> {
    return this.projectService.getByName(projectName);
  }
  public deleteProjectById(projectId: number): Observable<any> {
    return this.projectService.deleteById(projectId);
  }
  public deleteProjectByName(projectName: string): Observable<any> {
    return this.projectService.deleteByName(projectName);
  }
  public addProject(project: ProjectDto): Observable<any> {
    return this.projectService.addProject(project);
  }
  public addProjectWithAssignment(
    userId: number,
    project: ProjectDto
  ): Observable<any> {
    return this.projectService
      .addProjectWithAssignment(userId, project)
      .pipe(tap(() => this._refreshRequired.next()));
  }
  public updateProject(
    projectId: number,
    project: ProjectDto
  ): Observable<any> {
    return this.projectService.updateProject(projectId, project);
  }
}

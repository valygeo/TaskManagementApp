import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sprint } from '../models/Sprint';
import { SprintDto } from '../models/SprintDto';
import { SprintApiService } from './sprint-api.service';

@Injectable({
  providedIn: 'root',
})
export class SprintService {
  constructor(private sprintService: SprintApiService) {}
  public getAllSprints(): Observable<Sprint[]> {
    return this.sprintService.get('allSprints');
  }
  public getSprintById(sprintId: number): Observable<Sprint> {
    return this.sprintService.getById(sprintId);
  }
  public getSprintByName(sprintName: string): Observable<Sprint> {
    return this.sprintService.getByName(sprintName);
  }
  public deleteSprintById(sprintId: number): Observable<any> {
    return this.sprintService.deleteById(sprintId);
  }
  public deleteSprintByName(sprintName: string): Observable<any> {
    return this.sprintService.deleteByName(sprintName);
  }
  public addSprint(sprint: SprintDto): Observable<any> {
    return this.sprintService.addSprint(sprint);
  }
  public getSprintByProjectId(projectId: number): Observable<Sprint[]> {
    return this.sprintService.getByProjectId(projectId);
  }
}

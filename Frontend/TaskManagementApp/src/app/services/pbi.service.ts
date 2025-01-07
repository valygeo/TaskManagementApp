import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pbi } from '../models/Pbi';
import { PbiDto } from '../models/PbiDto';
import { PbiApiService } from './pbi-api.service';

@Injectable({
  providedIn: 'root',
})
export class PbiService {
  constructor(private pbiService: PbiApiService) {}
  public getAll(): Observable<Pbi[]> {
    return this.pbiService.get('allPbi');
  }
  public getPbiByUserIdAndSprintId(
    userId: number,
    sprintId: number
  ): Observable<Pbi[]> {
    return this.pbiService.getPbiByUserIdSprintId(userId, sprintId);
  }
  public addPbi(pbi: PbiDto): Observable<any> {
    return this.pbiService.addPbi(pbi);
  }
  public updatePbi(pbiId: number, pbi: PbiDto): Observable<any> {
    return this.pbiService.updatePbi(pbiId, pbi);
  }
  public deletePbi(pbiId: number): Observable<any> {
    return this.pbiService.deleteById(pbiId);
  }
  public getPbiById(pbiId: number): Observable<Pbi> {
    return this.pbiService.getById(pbiId);
  }
}

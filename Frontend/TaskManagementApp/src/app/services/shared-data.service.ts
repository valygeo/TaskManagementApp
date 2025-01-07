import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Sprint } from '../models/Sprint';

@Injectable({
  providedIn: 'root',
})
export class SharedDataService {
  sprint: any[] = [];
  private messageSource = new BehaviorSubject(this.sprint);
  currentMessage = this.messageSource.asObservable();
  constructor() {}
  changeMessage(message: any) {
    this.messageSource.next(message);
  }
}

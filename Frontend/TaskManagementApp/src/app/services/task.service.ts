import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from '../models/Task';
import { TaskDto } from '../models/TaskDto';
import { TaskApiService } from './task-api.service';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(public taskService: TaskApiService) {}
  public getAllTasks(): Observable<Task[]> {
    return this.taskService.get('allTasks');
  }
  // getTaskById(code:any): Observable<ITask[]> {
  //   return this.taskService.getById('getById'+code);
  // }
  // getToDoTasks(): Observable<Task[]> {
  //   return this.taskService.getToDo();
  // }
  public updateTask(taskId: number, task: TaskDto): Observable<any> {
    return this.taskService.updateTask(taskId, task);
  }
  public addTask(task: TaskDto): Observable<any> {
    return this.taskService.addTask(task);
  }
  public getTaskByUserIdPbiId(
    userId: number,
    taskId: number
  ): Observable<Task[]> {
    return this.taskService.getTaskByUserIdPbiId(userId, taskId);
  }
  public getTaskById(taskId: number): Observable<Task> {
    return this.taskService.getById(taskId);
  }
  public deleteTaskById(taskId: number): Observable<any> {
    return this.taskService.deleteTask(taskId);
  }
}

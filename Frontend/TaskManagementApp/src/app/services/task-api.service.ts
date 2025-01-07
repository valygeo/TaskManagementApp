import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from '../models/Task';
import { Params } from '@angular/router';
import { TaskDto } from '../models/TaskDto';

@Injectable({
  providedIn: 'root',
})
export class TaskApiService {
  constructor(private http: HttpClient) {}
  private urlGetAll = 'https://localhost:7235/api/Task/GetAllTasks/';
  private urlGetById = 'https://localhost:7235/api/Task/GetTaskById/';
  private urlGetAllToDoTasks =
    'https://localhost:7235/api/Task/GetAllToDoTasks';
  private urlUpdateTask = 'https://localhost:7235/api/Task/UpdateTask/';
  private urlGetTaskByUserIdPbiId =
    'https://localhost:7235/api/Task/GetTaskByUserIdPbiId/';
  private urlAddTask = 'https://localhost:7235/api/Task/AddTask/';
  private urlDeleteTask = 'https://localhost:7235/api/Task/DeleteTask/';
  public get(url: string, Params?: Params): Observable<Task[]> {
    return this.http.get<Task[]>(this.urlGetAll);
  }
  public getById(taskId: number): Observable<any> {
    return this.http.get<Task>(this.urlGetById + taskId);
  }
  // getToDo(): Observable<Task[]> {
  //   return this.http.get<Task[]>(this.urlGetAllToDoTasks);
  // }
  public updateTask(taskId: number, task: TaskDto): Observable<any> {
    return this.http.put(this.urlUpdateTask + taskId, task, {
      responseType: 'text',
    });
  }
  public addTask(task: TaskDto): Observable<any> {
    return this.http.post(this.urlAddTask, task, {
      responseType: 'text',
    });
  }
  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(this.urlDeleteTask + taskId, {
      responseType: 'text',
    });
  }
  public getTaskByUserIdPbiId(
    userId: number,
    pbiId: number
  ): Observable<Task[]> {
    return this.http.get<Task[]>(
      this.urlGetTaskByUserIdPbiId + userId + '/' + pbiId
    );
  }
}

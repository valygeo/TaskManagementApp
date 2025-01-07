import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Component, Input, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { Task } from 'src/app/models/Task';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/User';
import { Subscription } from 'rxjs';
import { PbiService } from 'src/app/services/pbi.service';
import { Pbi, PbiType } from 'src/app/models/Pbi';
import { TaskDto } from 'src/app/models/TaskDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ThisReceiver } from '@angular/compiler';
import { SprintService } from 'src/app/services/sprint.service';
import { Sprint } from 'src/app/models/Sprint';
import { SharedDataService } from 'src/app/services/shared-data.service';
import {
  ModalDismissReasons,
  NgbDateStruct,
  NgbModal,
} from '@ng-bootstrap/ng-bootstrap';
import {
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { PbiDto } from 'src/app/models/PbiDto';
import { SprintDto } from 'src/app/models/SprintDto';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/Project';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  constructor(
    private taskService: TaskService,
    private userService: UserService,
    private pbiService: PbiService,
    private sprintService: SprintService,
    private dataService: SharedDataService,
    private modalService: NgbModal,
    private projectService: ProjectService
  ) {}
  todo: Task[] = [];
  test: Task[] = [];
  done: Task[] = [];

  sprintById: Sprint;

  pbiById: Pbi;
  progress: Task[] = [];
  pbiName: string = '';
  modelStartDate: NgbDateStruct;
  usersWorkingOnAProject: User[] = [];
  modelEndDate: NgbDateStruct;
  allTasks: Task[] = [];
  projectById: Project;
  users: User[] = [];
  closeResult: string;
  pbi: Pbi[] = [];
  task: Task[];
  taskById: Task;
  user: User;
  sprintId: number;
  currentPbiName: string;
  pbi1: Pbi[] = [];
  sprintOfaProject: Sprint[];
  pbiForm!: UntypedFormGroup;
  userWorking: User;
  bugs: Pbi[] = [];
  features: Pbi[] = [];
  editPbi!: UntypedFormGroup;
  editTask!: UntypedFormGroup;
  taskForm!: UntypedFormGroup;
  taskToUpdate: TaskDto;
  userByUsername: User;
  projectId: number;
  subscription: Subscription = new Subscription();
  sprintForm!: UntypedFormGroup;
  types: { Bug; Feature };
  private username = localStorage.getItem('username') || '';
  sprint: Sprint[];
  ngOnInit(): void {
    this.userService.getUserByUsername(this.username).subscribe((data) => {
      this.userByUsername = data;
      this.dataService.currentMessage.subscribe((currentSprint) => {
        this.sprint = currentSprint;
        console.log('sprint', this.sprint);

        this.sprint.forEach((sprint) =>
          this.pbiService
            .getPbiByUserIdAndSprintId(this.userByUsername.id, sprint.sprint_Id)
            .subscribe((data) => {
              (this.projectId = sprint.project_Id),
                // console.log('pbi', data), (this.pbi = data)
                data.forEach((e1) => {
                  if (e1.pbi_type == 0) {
                    this.bugs.push(e1);
                  } else this.features.push(e1);
                  this.pbi.push(e1);
                });
            })
        );
      });
    });
    this.subscription.add(this.getAllUsers());

    // public getTasksByUserIdPbiId(userId: number, pbiId: number) {
    //   this.todo = [];
    //   this.done = [];
    //   this.progress = [];
    //   this.taskService
    //     .getTaskByUserIdPbiId(this.userByUsername.id, pbiId)
    //     .subscribe((tasks) =>
    //       tasks.filter((task) => {
    //         if (task.task_Status == 'To do') {
    //           this.todo.push(task);

    //           // console.log('To do initial', this.todo);
    //         } else if (task.task_Status == 'In progress') {
    //           this.progress.push(task);
    //         } else this.done.push(task);
    //       })
    //     );
    // }
    // this.projectsDifference = this.allProjects.filter(
    //   (project) =>
    //     !this.userProjects.some(
    //       (present) => project.project_Id == present.project_Id
    //     )
    // );

    //   return this.allTasks.filter((task) => {
    //     if (task.task_Status == 'To do') {
    //       this.todo.push(task);
    //       // console.log('To do initial', this.todo);
    //     } else if (task.task_Status == 'In progress') {
    //       this.progress.push(task);
    //     } else this.done.push(task);
    //     // console.log('In progress initial', this.progress);
    //     // console.log('Doneinitial', this.done);
    //   });
    // });

    this.pbiForm = new UntypedFormGroup({
      pbi_Name: new UntypedFormControl('', [Validators.required]),
      pbi_Description: new UntypedFormControl('', [Validators.required]),
      start_Date_Pbi: new UntypedFormControl('', [Validators.required]),
      end_Date_Pbi: new UntypedFormControl('', [Validators.required]),
      pbi_type: new UntypedFormControl('', [Validators.required]),
      sprint_Id: new UntypedFormControl('', [Validators.required]),
    });
    this.sprintForm = new UntypedFormGroup({
      sprint_Name: new UntypedFormControl('', [Validators.required]),
      sprint_Description: new UntypedFormControl('', [Validators.required]),
      start_Date_Sprint: new UntypedFormControl('', [Validators.required]),
      end_Date_Sprint: new UntypedFormControl('', [Validators.required]),
    });
    this.taskForm = new UntypedFormGroup({
      task_Name: new UntypedFormControl('', [Validators.required]),
      task_Description: new UntypedFormControl('', [Validators.required]),
      pbi_Id: new UntypedFormControl('', [Validators.required]),
    });

    this.editPbi = new UntypedFormGroup({
      pbi_Name: new UntypedFormControl(''),
      pbi_Description: new UntypedFormControl(''),
      start_Date_Pbi: new UntypedFormControl(''),
      end_Date_Pbi: new UntypedFormControl(''),
      pbi_type: new UntypedFormControl(''),
      sprint_Id: new UntypedFormControl(''),
      id: new UntypedFormControl(''),
    });
    this.editTask = new UntypedFormGroup({
      task_Name: new UntypedFormControl(''),
      task_Description: new UntypedFormControl(''),
      task_Status: new UntypedFormControl(''),
    });
  }
  validateControl = (controlName: string) => {
    return (
      this.pbiForm.get(controlName)?.invalid &&
      this.pbiForm.get(controlName)?.touched
    );
  };
  validateControlSprint = (controlName: string) => {
    return (
      this.sprintForm.get(controlName)?.invalid &&
      this.sprintForm.get(controlName)?.touched
    );
  };
  hasErrorSprint = (controlName: string, errorName: string) => {
    return this.sprintForm.get(controlName)?.hasError(errorName);
  };
  hasError = (controlName: string, errorName: string) => {
    return this.pbiForm.get(controlName)?.hasError(errorName);
  };
  validateControlTask = (controlName: string) => {
    return (
      this.taskForm.get(controlName)?.invalid &&
      this.taskForm.get(controlName)?.touched
    );
  };
  hasErrorTask = (controlName: string, errorName: string) => {
    return this.taskForm.get(controlName)?.hasError(errorName);
  };
  public getTasksByUserIdPbiId(userId: number, pbiId: number) {
    this.todo = [];
    this.done = [];
    this.progress = [];
    this.taskService
      .getTaskByUserIdPbiId(this.userByUsername.id, pbiId)
      .subscribe((tasks) =>
        tasks.filter((task) => {
          if (task.task_Status == 'To do') {
            this.todo.push(task);

            // console.log('To do initial', this.todo);
          } else if (task.task_Status == 'In progress') {
            this.progress.push(task);
          } else this.done.push(task);
        })
      );
  }
  private getAllUserTask() {
    this.taskService.getAllTasks().subscribe((data) => {
      this.allTasks = data;
      return this.allTasks.filter((task) => {
        if (task.task_Status == 'To do') {
          this.todo.push(task);
          // console.log('To do initial', this.todo);
        } else if (task.task_Status == 'In progress') {
          this.progress.push(task);
        } else this.done.push(task);
        // console.log('In progress initial', this.progress);
        // console.log('Doneinitial', this.done);
      });
    });
  }
  public getDbUsersByProjectId(projectId: number) {
    this.userService
      .getUsersByProjectId(projectId)
      //   data.forEach((currData) => {
      //     this.usersWorkingOnAProject.push(currData);
      //   })

      .subscribe((data) => (this.usersWorkingOnAProject = data));
  }

  // this.subscription.add(this.getAllPbi());
  // this.subscription.add(
  //   this.dataService.currentMessage.subscribe((message) => {
  //     this.sprint = message;
  //     console.log('sprints in adshboard', message);
  //   })
  // );
  // this.subscription.add(
  //   this.getUserByUsername(localStorage.getItem('username') || '')
  // );
  // this.subscription.add(this.getAllPbi());
  // this.getAllUserTask();
  // this.taskService.getAllTasks().subscribe((data) => {
  //   this.done = data;
  //   this.todo = data;
  //   this.progress = data;
  // });

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  drop(event: CdkDragDrop<Task[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
      this.task = event.container.data;
      this.todo.filter((task) => {
        if (task.task_Status != 'To do') {
          task.task_Status = 'To do';
          this.taskToUpdate = task;
          this.updateDbTask(task.task_Id, this.taskToUpdate);
        }
      });
      this.progress.filter((task) => {
        if (task.task_Status != 'In progress') {
          task.task_Status = 'In progress';
          this.taskToUpdate = task;
          this.updateDbTask(task.task_Id, this.taskToUpdate);
        }
      });
      this.done.filter((task) => {
        if (task.task_Status != 'Done') {
          task.task_Status = 'Done';
          this.taskToUpdate = task;
          this.updateDbTask(task.task_Id, this.taskToUpdate);
        }
      });
    }
  }
  // private getAllToDoTasks() {
  //   this.taskService.getToDoTasks().subscribe((data) => (this.todo = data));
  // }
  // private getAllUserTask() {
  //   this.taskService.getAllTasks().subscribe((data) => {
  //     this.allTasks = data;
  //     return this.allTasks.filter((task) => {
  //       if (task.task_Status == 'To do') {
  //         this.todo.push(task);
  //         // console.log('To do initial', this.todo);
  //       } else if (task.task_Status == 'In progress') {
  //         this.progress.push(task);
  //       } else this.done.push(task);
  //       // console.log('In progress initial', this.progress);
  //       // console.log('Doneinitial', this.done);
  //     });
  //   });
  // }
  // private getUserByUsername(username: string): User {
  //   this.userService.getUserByUsername(username).subscribe((data) => {
  //     (this.userByUsername = data), (this.done = this.userByUsername.tasks);
  //   });
  //   return this.userByUsername;
  // }

  private getAllPbi() {
    this.pbiService.getAll().subscribe((data) => {
      console.log(data);
      this.pbi = data;
    });
  }
  private getAllUserPbi() {
    this.userService.getUserByUsername(this.username).subscribe((data) => {
      this.pbi = data.pbi;
      console.log(this.pbi);
    });
  }

  // public onEditClicked(pbiId: number) {
  //   this.pbiService.getPbiById(pbiId).subscribe((data) => {
  //     this.pbiById = data;
  //     console.log(this.pbiById);
  //     // this.project.start_Date_Project=this.datePipe.transform(this.project.start_Date_Project,'yyyy-MM-dd')

  //     this.editPbi.setValue({
  //       pbi_Name: this.pbiById.pbi_Name,
  //       pbi_Description: this.pbiById.pbi_Description,
  //       // start_Date_Pbi: this.pbiById.start_Date_Pbi,
  //       // end_Date_Pbi: this.pbiById.end_Date_Pbi,
  //       // pbi_type: this.pbiById.PbiType,
  //       // sprint_Id: this.pbiById.sprint_Id,
  //     });
  //   });
  // }
  public updateDbTask(taskId: number, task: TaskDto) {
    this.taskService.updateTask(taskId, task).subscribe(
      (response: string) => {
        alert('Task was updated!');
        console.log(response);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }
  public editDbTask(taskId: number, task: TaskDto) {
    task.id = this.userByUsername.id;
    task.pbi_Id = this.pbiById.pbi_Id;
    this.taskService.updateTask(taskId, task).subscribe(
      (response: string) => {
        alert('Task was updated!');
        console.log(response);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }
  // newMessage() {
  //   this.dataService.changeMessage('Hello from Sibling');
  // }
  // public getPbiByUserIdAndSprintId(userId: number, sprintId: number) {
  //   this.dataService.currentMessage.subscribe((currentSprint) => {
  //     this.sprint = currentSprint;
  //   });
  //   this.user = this.getUserByUsername(this.username);
  //   this.sprint.forEach((sprint) =>
  //     this.pbiService
  //       .getPbiByUserIdAndSprintId(this.user.id, sprint.sprint_Id)
  //       .subscribe((data) => console.log(data))
  //   );
  // }
  open(content) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  public convertStartDate() {
    const startDate = new Date(
      this.modelStartDate.year,
      this.modelStartDate.month - 1,
      this.modelStartDate.day + 1
    );
    return startDate;
  }
  public convertEndDate() {
    const endDate = new Date(
      this.modelEndDate.year,
      this.modelEndDate.month - 1,
      this.modelEndDate.day + 1
    );
    return endDate;
  }
  public convertNameToId() {
    const sprint = new Sprint();
  }
  changeClient(value) {
    // console.log(value);
    return value;
  }

  public addPbi(pbi: PbiDto) {
    pbi.start_Date_Pbi = this.convertStartDate();
    pbi.end_Date_Pbi = this.convertEndDate();
    pbi.id = this.userByUsername.id;

    // pbi.sprint_Id = this.changeClient(this.sprintId);
    // pbi.sprint_Id =
    // this.sprintService.getSprintByName(pbi.sprint_Id).subscribe((data)=>
    // this.sprintId= data.sprint_Id)
    console.log(pbi);
    this.pbiService.addPbi(pbi).subscribe(
      (response: string) => {
        alert('Pbi was added!');
        console.log(response);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }
  public addSprint(sprint: SprintDto) {
    sprint.start_Date_Sprint = this.convertStartDate();
    sprint.end_Date_Sprint = this.convertEndDate();
    sprint.project_Id = this.projectId;
    console.log(sprint);
    this.sprintService.addSprint(sprint).subscribe(
      (response: string) => {
        alert('Sprint was added!');
        console.log(response);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }

  public addTask(task: TaskDto) {
    task.id = this.userByUsername.id;

    // pbi.sprint_Id = this.changeClient(this.sprintId);
    // pbi.sprint_Id =
    // this.sprintService.getSprintByName(pbi.sprint_Id).subscribe((data)=>
    // this.sprintId= data.sprint_Id)

    this.taskService.addTask(task).subscribe(
      (response: string) => {
        alert('Task was added!');
        console.log(response);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }

  public userWorkingOnATask(userId: number) {
    this.userService
      .getUserById(userId)
      .subscribe((data) => (this.userWorking = data));
  }
  public getPbiById(pbiId: number) {
    this.pbiService.getPbiById(pbiId).subscribe((data) => {
      (this.pbiName = data.pbi_Name), (this.pbiById = data);
    });
  }
  public getTaskById(taskId: number) {
    this.taskService
      .getTaskById(taskId)
      .subscribe((data) => (this.taskById = data));
  }
  public deleteTask(taskId: number) {
    this.taskService.deleteTaskById(taskId).subscribe(
      (response: string) => {
        alert('Task was deleted!');
        console.log(response);
        // this.getUserProject(this.username);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }

  public updateDbPbi(pbiId: number, pbi: PbiDto) {
    pbi.start_Date_Pbi = this.convertStartDate();
    pbi.end_Date_Pbi = this.convertEndDate();

    this.pbiService.updatePbi(pbiId, pbi).subscribe(
      (response: string) => {
        alert('Pbi was updated!');
        console.log(response);
        // this.getUserProject(this.username);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
  }
  public deletePbiById(pbiId: number) {
    this.pbiService.deletePbi(pbiId).subscribe(
      (response: string) => {
        alert('Pbi was deleted!');
        console.log(response);
        // this.getUserProject(this.username);
      },
      (error: HttpErrorResponse) => {
        alert('Pbi was not found!');
        console.log(error.message);
      }
    );
  }
  public getSprintDetails(sprintId: number) {
    this.sprintService.getSprintById(sprintId).subscribe((data) => {
      (this.sprintById = data), console.log(data);
    });
  }
  public getProjectById(projectId: number) {
    this.projectService.getProjectById(projectId).subscribe((data) => {
      (this.projectById = data), console.log('projectbyid', data);
    });
  }
  public getAllUsers() {
    this.userService.getAllUsers().subscribe((data) => (this.users = data));
  }
}

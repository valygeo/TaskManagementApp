import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/User';
import { ApiService } from 'src/app/services/api.service';
import {
  BehaviorSubject,
  map,
  Observable,
  Subject,
  subscribeOn,
  Subscription,
  switchMap,
} from 'rxjs';
import { LoginUserComponent } from 'src/app/authentication/login/login.component';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { TaskService } from 'src/app/services/task.service';
import { Task } from 'src/app/models/Task';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/Project';
import { HttpErrorResponse } from '@angular/common/http';
import {
  ModalDismissReasons,
  NgbDate,
  NgbDateStruct,
  NgbModal,
} from '@ng-bootstrap/ng-bootstrap';
import { ProjectDto } from 'src/app/models/ProjectDto';
import { MatDatepicker } from '@angular/material/datepicker';
import { AuthenticationService } from 'src/app/services/authentication.service';
import {
  FormControl,
  FormGroup,
  NgForm,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { DatePipe } from '@angular/common';
import { SprintService } from 'src/app/services/sprint.service';
import { Sprint } from 'src/app/models/Sprint';

import { SharedDataService } from 'src/app/services/shared-data.service';
import { SprintDto } from 'src/app/models/SprintDto';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
})
export class BoardComponent implements OnInit {
  sprintForm: UntypedFormGroup;
  constructor(
    private userService: UserService,
    private taskService: TaskService,
    private projectService: ProjectService,
    private modalService: NgbModal,
    private authenticationService: AuthenticationService,
    private datePipe: DatePipe,
    private sprintService: SprintService,
    private dataService: SharedDataService
  ) {}
  subscription: Subscription = new Subscription();
  todo: Task[] = [];
  modelStartDate: NgbDateStruct;
  modelEndDate: NgbDateStruct;
  done: Task[] = [];
  projectForSprint: Project;
  progress: Task[] = [];
  // users: User[] = [];
  tasks: Task[] = [];
  code: any;
  projectName: string = '';
  usersWorkingOnAProject: User[] = [];
  userByUsername: User;
  projects: Project[] = [];
  closeResult: string;
  project: Project;
  projectId: number;
  editProject!: UntypedFormGroup;
  dateToConvert: Date;
  dateToConvert1: Date;
  date: Date;
  userId: number;
  message: Sprint[];
  sprintsByProjectId: Sprint[];

  // projectForm = new UntypedFormGroup({
  //   project_Name: new UntypedFormControl('', [Validators.required]),
  //   project_Description: new UntypedFormControl('', [Validators.required]),
  //   start_Date_Project: new UntypedFormControl('', [Validators.required]),
  //   end_Date_Project: new UntypedFormControl('', [Validators.required]),
  // });
  private username = localStorage.getItem('username') || '';
  ngOnInit(): void {
    this.subscription.add(
      this.dataService.currentMessage.subscribe(
        (message) => (this.message = message)
      )
    );
    this.editProject = new UntypedFormGroup({
      project_Name: new UntypedFormControl(''),
      project_Description: new UntypedFormControl(''),
      start_Date_Project: new UntypedFormControl(''),
      end_Date_Project: new UntypedFormControl(''),
    });
    this.sprintForm = new UntypedFormGroup({
      sprint_Name: new UntypedFormControl('', [Validators.required]),
      sprint_Description: new UntypedFormControl('', [Validators.required]),
      start_Date_Sprint: new UntypedFormControl('', [Validators.required]),
      end_Date_Sprint: new UntypedFormControl('', [Validators.required]),
    });
    // this.editProject.setValue({
    //   project_Name: this.project.project_Name,
    //   project_Description: this.project.project_Description,
    //   start_Date_Project: this.project.start_Date_Project,
    //   end_Date_Project: this.project.end_Date_Project,
    // });

    // this.subscription.add(this.getAllDbProjects());
    // this.subscription.add(
    //   this.projectService.refreshRequired.subscribe((response) =>
    //     this.getAllDbProjects()
    //   )
    // );
    this.subscription.add(this.getUserProject(this.username));
    this.subscription.add(
      this.projectService.refreshRequired.subscribe((response) =>
        this.getUserProject(this.username)
      )
    );
    this.subscription.add(this.getUserByUsername());
    this.subscription.add(
      this.dataService.currentMessage.subscribe((message) => {
        this.message = message;
      })
    );

    // this.subscription.add(
    //   this.userService1.getAllUsers().subscribe((data) => {
    //     console.log(data);
    //   })
    // );
    // this.subscription.add(
    //   this.taskService1.getAllTasks().subscribe((data) => {
    //     console.log(data);
    //     this.done = data;
    //     this.todo = data;
    //     this.progress = data;
    //     this.tasks = data;
    //   })
    // );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  // getAllDbProjects() {
  //   this.projectService.getAllProjects().subscribe((data) => {
  //     console.log(data);
  //     data.forEach((e1) => {
  //       this.projects.push(e1);
  //     });
  //   });
  // }

  // private getUserByUsername(username: string) {
  //   this.userService.getUserByUsername(username).subscribe((data) => {
  //     console.log(data);
  //     (this.userByUsername = data),
  //       (this.projects = this.userByUsername.projects);
  //   });
  // }
  private getUserProject(username: string) {
    this.userService
      .getUserByUsername(username)
      .pipe(
        map((projects) =>
          projects.projects.sort(
            (a, b) =>
              new Date(b.start_Date_Project).getTime() -
              new Date(a.start_Date_Project).getTime()
          )
        )
      )
      // .subscribe((data) =>
      //   data.forEach((currData) => {
      //     this.projects.push(currData);
      //   })
      // );
      .subscribe((projects) => {
        this.projects = projects;
        console.log(this.projects);
      });
  }
  private getUserByUsername() {
    this.userService.getUserByUsername(this.username).subscribe((user) => {
      this.userId = user.id;
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
    }
  }
  deleteProjectById(projectId: number) {
    this.projectService.deleteProjectById(projectId).subscribe(
      (response: string) => {
        alert('Project was deleted!');
        console.log(response);
        this.getUserProject(this.username);
      },
      (error: HttpErrorResponse) => {
        alert('Project was not found!');
        console.log(error.message);
      }
    );
  }
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
  public updateDbProject(projectId: number, project: ProjectDto) {
    project.start_Date_Project = this.convertStartDate();
    project.end_Date_Project = this.convertEndDate();
    this.projectService.updateProject(projectId, project).subscribe(
      (response: string) => {
        alert('Project was updated!');
        console.log(response);
        this.getUserProject(this.username);
      },
      (error: HttpErrorResponse) => {
        alert('error');
        console.log(error.message);
      }
    );
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
  // private getUserByUsername(username: string) {
  //   this.userService1.getUserByUsername(username).subscribe((data) => {
  //     console.log(data);
  //     (this.userById = data), (this.projects = this.userById.projects);
  //   });
  // }
  public getProjectByName() {
    this.projectService.getProjectByName(this.projectName).subscribe((data) => {
      console.log(data);
    });
  }
  // public onEditClicked(projectName: string) {
  //   this.projectService.getProjectByName(projectName).subscribe((data) => {
  //     this.project = data;
  //   });
  //   console.log(this.project.project_Name);
  // }
  public onEditClicked(projectId: number) {
    this.projectService.getProjectById(projectId).subscribe((data) => {
      this.project = data;
      // this.project.start_Date_Project=this.datePipe.transform(this.project.start_Date_Project,'yyyy-MM-dd')
      this.editProject.setValue({
        project_Name: this.project.project_Name,
        project_Description: this.project.project_Description,
        start_Date_Project: this.project.start_Date_Project,
        end_Date_Project: this.project.end_Date_Project,
      });
    });
  }
  public getProjectById(projectId: number) {
    this.projectService
      .getProjectById(projectId)
      .subscribe((data) => (this.projectForSprint = data));
  }
  public getSprintsByProjectId(projectId: number) {
    this.sprintService.getSprintByProjectId(projectId).subscribe({
      next: (data) => {
        console.log(data),
          (this.sprintsByProjectId = data),
          this.dataService.changeMessage(this.sprintsByProjectId);
        // this.dataService.changeMessage(this.sprintsByProjectId);
      },
      error: (err: HttpErrorResponse) => {
        alert('Sprints doesnt exists for this project');
      },
    });
  }
  validateControlSprint = (controlName: string) => {
    return (
      this.sprintForm.get(controlName)?.invalid &&
      this.sprintForm.get(controlName)?.touched
    );
  };
  hasErrorSprint = (controlName: string, errorName: string) => {
    return this.sprintForm.get(controlName)?.hasError(errorName);
  };

  public addSprint(sprint: SprintDto) {
    sprint.start_Date_Sprint = this.convertStartDate();
    sprint.end_Date_Sprint = this.convertEndDate();
    sprint.project_Id = this.projectForSprint.project_Id;
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
  public disenrollUser(userId: number, projectId: number) {
    this.userService.disenrollUser(userId, projectId).subscribe({
      next: (response: string) => {
        console.log(response);
        alert('Succes');
        this.getUserProject(this.username);
        // this.getProjectsNotAssignedToUser();
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
        alert('fail');
        // this.getProjectsNotAssignedToUser();
      },
    });
  }
  // newMessage() {
  //   this.dataService.changeMessage('hello');
  // }
}

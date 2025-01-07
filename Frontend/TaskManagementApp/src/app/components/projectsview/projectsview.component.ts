import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  ModalDismissReasons,
  NgbDateStruct,
  NgbModal,
} from '@ng-bootstrap/ng-bootstrap';
import { map, Subscription } from 'rxjs';
import { Project } from 'src/app/models/Project';
import { ProjectDto } from 'src/app/models/ProjectDto';
import { User } from 'src/app/models/User';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProjectService } from 'src/app/services/project.service';
import { TaskService } from 'src/app/services/task.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-projectsview',
  templateUrl: './projectsview.component.html',
  styleUrls: ['./projectsview.component.css'],
})
export class ProjectsviewComponent implements OnInit {
  constructor(
    private userService: UserService,
    private taskService: TaskService,
    private projectService: ProjectService,
    private modalService: NgbModal,
    private authenticationService: AuthenticationService
  ) {}
  subscription: Subscription = new Subscription();
  todo: Task[] = [];
  user: User;
  modelStartDate: NgbDateStruct;
  modelEndDate: NgbDateStruct;
  done: Task[] = [];
  allProjects: Project[] = [];
  projectsDifference: Project[];
  userByUsername: User;
  progress: Task[] = [];

  tasks: Task[] = [];
  usersWorkingOnAProject: User[] = [];
  code: any;
  userId: number;
  userProjects: Project[] = [];
  newProjects: Project[];
  closeResult: string;
  showError: boolean = true;
  projectsSortedAfterStartDate: Project[];
  project: Project;
  projectId: number;
  userProjects1: Project[];
  projectName: string = '';
  private username = localStorage.getItem('username') || '';
  ngOnInit(): void {
    // this.getCommonProjects();
    this.getProjectsNotAssignedToUser();
    // this.subscription.add(this.getAllProjects());
    // this.subscription.add(this.getProjectsNotAssignedToUser());
    // this.subscription.add(
    //   this.projectService.refreshRequired.subscribe((result) =>
    //     this.getProjectsNotAssignedToUser()
    //   )
    // );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  private getCommonProjects() {
    this.userService.getUserByUsername(this.username).subscribe((data) => {
      (this.userByUsername = data),
        (this.userId = data.id),
        (this.userProjects = this.userByUsername.projects);
      console.log(this.userProjects);
    });
  }
  private getProjectsNotAssignedToUser() {
    this.userService.getUserByUsername(this.username).subscribe((data) => {
      (this.userByUsername = data),
        (this.userId = data.id),
        (this.userProjects = this.userByUsername.projects);
    });
    this.projectService.getAllProjects().subscribe((data) => {
      this.allProjects = data;
      this.projectsDifference = this.allProjects.filter(
        (project) =>
          !this.userProjects.some(
            (present) => project.project_Id == present.project_Id
          )
      );

      this.projectsDifference.sort(
        (a, b) =>
          new Date(b.start_Date_Project).getTime() -
          new Date(a.start_Date_Project).getTime()
      );
      this.projectsSortedAfterStartDate = this.projectsDifference;
      console.log(this.projectsSortedAfterStartDate);

      console.log('users projects', this.userProjects);
    });

    // this.projectService
    //   .getAllProjects()
    //   .pipe(
    //     map((projects) =>
    //       projects.sort(
    //         (a, b) =>
    //           new Date(b.start_Date_Project).getTime() -
    //           new Date(a.start_Date_Project).getTime()
    //       )
    //     )
    //   )
    //   .subscribe((projects) => {
    //     this.userProjects = projects;
    //   });
  }
  public getDbUsersByProjectId(projectId: number) {
    this.userService
      .getUsersByProjectId(projectId)
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
        this.getAllProjects();
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
        this.getAllProjects();
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
  //   this.userService.getUserByUsername(username).subscribe((data) => {
  //     (this.userByUsername = data),
  //       (this.userId = data.id),
  //       (this.projects = this.userByUsername.projects);

  //     console.log('users projectss', this.projects);
  //   });
  //   this.projectService.getAllProjects().subscribe((data) => {
  //     this.allProjects = data;
  //     console.log('all projects', this.allProjects);
  //   });
  //   let projectsDifference = this.allProjects.filter(
  //     (project) =>
  //       !this.projects.some(
  //         (present) => present.project_Id == project.project_Id
  //       )
  //   );
  //   console.log('difference', projectsDifference);
  // }
  // public enrollUser(userId: number, projectId: number) {
  //   this.userService.getUserByUsername(this.username).subscribe((data) => {
  //     console.log(data), (this.user = data);
  //     this.projectService
  //       .getProjectByName(this.project1.project_Name)
  //       .subscribe((data) => (this.project = data));
  //     this.userService
  //       .enrollUser(this.user.id, this.project.project_Id)
  //       .subscribe((data) => console.log(data));
  //   });
  // }
  public enrollUser(userId: number, projectId: number) {
    this.userService.enrollUser(userId, projectId).subscribe({
      next: (response: string) => {
        console.log(response);
        alert('Succes');
        this.getAllProjects();
      },
      error: (err: HttpErrorResponse) => {
        this.getAllProjects();
      },
    });
  }

  public getAllProjects() {
    this.projectService
      .getAllProjects()
      .pipe(
        map((projects) =>
          projects.sort(
            (a, b) =>
              new Date(b.start_Date_Project).getTime() -
              new Date(a.start_Date_Project).getTime()
          )
        )
      )
      .subscribe((projects) => (this.allProjects = projects));
  }
  public checkProject(project: Project) {
    this.userService.getUserByUsername(this.username).subscribe((data) => {
      (this.userByUsername = data),
        (this.userId = data.id),
        (this.userProjects = this.userByUsername.projects);
    });
  }
}

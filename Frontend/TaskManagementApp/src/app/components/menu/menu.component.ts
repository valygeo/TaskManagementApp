import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/Project';
import { ProjectDto } from 'src/app/models/ProjectDto';
import { ProjectService } from 'src/app/services/project.service';
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
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/User';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
})
export class MenuComponent implements OnInit {
  constructor(
    private projectService: ProjectService,
    private modalService: NgbModal,
    private userService: UserService
  ) {}
  closeResult: string;
  modelStartDate: NgbDateStruct;
  modelEndDate: NgbDateStruct;
  errorMessage: string = '';
  projects: Project[] = [];
  project: Project;
  subscription: Subscription = new Subscription();
  currentUser: User;
  currentUserId: number;
  project1: Project;
  projectId: number = 0;
  projectName: string = '';
  projectForm!: UntypedFormGroup;
  isAdded: boolean = false;
  showError: boolean = false;
  private username = localStorage.getItem('username') || '';
  ngOnInit(): void {
    this.projectForm = new UntypedFormGroup({
      project_Name: new UntypedFormControl('', [Validators.required]),
      project_Description: new UntypedFormControl('', [Validators.required]),
      start_Date_Project: new UntypedFormControl('', [Validators.required]),
      end_Date_Project: new UntypedFormControl('', [Validators.required]),
    });
    this.subscription.add(this.getUserByUsername(this.username));
  }
  // getProjects() {
  //   this.projectService.getAllProjects().subscribe((data) => {
  //     console.log(data);
  //     data.forEach((e1) => {
  //       this.projects.push(e1);
  //     });
  //   });
  // }
  validateControl = (controlName: string) => {
    return (
      this.projectForm.get(controlName)?.invalid &&
      this.projectForm.get(controlName)?.touched
    );
  };
  hasError = (controlName: string, errorName: string) => {
    return this.projectForm.get(controlName)?.hasError(errorName);
  };
  // getProjectById() {
  //   this.projectService.getProjectById(this.projectId).subscribe((data) => {
  //     this.project = data;
  //     console.log(data);
  //   });
  // }
  getProjectByName(projectName: string) {
    this.projectService.getProjectByName(projectName).subscribe((data) => {
      this.project = data;
    });
  }
  deleteProjectById() {
    this.projectService.deleteProjectById(this.projectId).subscribe(
      (response: string) => {
        alert('Project was deleted!');
        console.log(response);
      },

      (error: HttpErrorResponse) => {
        alert('Project was not found!');
        console.log(error.message);
      }
    );
  }
  deleteProjectByName() {
    this.projectService.deleteProjectByName(this.projectName).subscribe(
      (response: string) => {
        alert('Project was deleted!');
        console.log(response);
      },
      (error: HttpErrorResponse) => {
        alert('Project was not found!');
        console.log(error.message);
      }
    );
  }
  // public addProject(project: ProjectDto) {
  //   project.start_Date_Project = this.convertStartDate();
  //   project.end_Date_Project = this.convertEndDate();
  //   this.projectService.addProject(project).subscribe(
  //     (response: string) => {
  //       alert('Project was added!');
  //       console.log(response);
  //       this.isAdded = true;
  //     },
  //     (error: HttpErrorResponse) => {
  //       alert('error');
  //       console.log(error.message);
  //     }
  //   );
  // }
  public getUserByUsername(username: string) {
    this.userService
      .getUserByUsername(this.username)
      .subscribe((data) => (this.currentUserId = data.id));
  }
  public addProjectDbWithAssignment(userId: number, project: ProjectDto) {
    project.start_Date_Project = this.convertStartDate();
    project.end_Date_Project = this.convertEndDate();
    this.projectService
      .addProjectWithAssignment(this.currentUserId, project)
      .subscribe(
        (response: string) => {
          alert('Project was added and you were assigned to this project!');
          console.log(response);
          this.isAdded = true;
        },
        (error: HttpErrorResponse) => {
          alert('error');
          console.log(error.message);
        }
      );
  }
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
  // public getUserByUsername(username: string) {
  //   this.userService
  //     .getUserById(username)
  //     .subscribe((data) => (this.user = data));
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
}

import { SprintApiService } from './../../services/sprint-api.service';
import { Sprint } from './../../models/Sprint';
import {
  Component,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import {
  map,
  Subscription,
  ReplaySubject
 } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/User';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/Project';
import { HttpErrorResponse } from '@angular/common/http';
import { getLocaleCurrencyCode } from '@angular/common';
import { CalendarOptions } from '@fullcalendar/angular';
import {
  ModalDismissReasons,
  NgbDateStruct,
  NgbModal,
} from '@ng-bootstrap/ng-bootstrap';

import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css'],
})
export class CalendarComponent {
  subscription: Subscription = new Subscription();
  events: any[] = [
    // {title: "nume foart lung", start: '2022-09-21', end: '2022-09-25', color: '#0000FF'},
    // {title: "nume foart lung", start: '2022-09-23', end: '2022-09-29', color: '#00FF00'}
  ];
  calendarOptions!: CalendarOptions;
  projects: Project[] = [];
  // projects$: ReplaySubject<Project[]> = new ReplaySubject<Project[]>(1);
  sprints: Sprint[] = [];
  tasks: Task[] = [];
  private username = localStorage.getItem('username') || '';

  constructor(private userService: UserService,
    private sprintService: SprintApiService,
    private projectService: ProjectService,
    private modalService: NgbModal) {}

  onDateClick(res: { dateStr: string }) {
    alert('You clicked on : ' + res.dateStr);
  }

  ngOnInit() {
    // this.subscription.add(this.getUserProject(this.username));
    // console.log("aici e afisat");
    // console.log(this.auxProjects);
    // this.subscription.add(
    //   this.getAllSprintsForUser(this.username)
    // );
    //--------


    this.subscription.add(
      this.userService.getUserByUsername(this.username).subscribe((response) =>
        this.addSprints(response.projects)
        // console.log(response.projects)
      )
    );

    // this.subscription.add(
    //   console.log(this.projects)
    // );



    console.log(this.projects);

    setTimeout(() => {
      this.calendarOptions = {
        initialView: 'dayGridMonth',
        dateClick: this.onDateClick.bind(this),
        events: this.events,
        eventTextColor: '#000',
        displayEventTime: false,
      };
    }, 3500);

  }

  //----------

  private addSprints(project: Project[]){
    const colors : any[] = ['#93CAED', '#FDFD96', '#BFE3B4', '#FF8B3D', '#AF8FE9', '#AFE4DE', 'BB437E'];

    console.log(project);
    console.log(project.length)

    project.forEach((currProject) =>
    this.sprintService
    .getByProjectId(currProject.project_Id)
    .subscribe((data) => {
      data.forEach((currSprint) =>{
        // console.log("curent sprint: " + currSprint);
        const index: number = Math.floor(Math.random() * colors.length);
        const newEvent = {title: currSprint.sprint_Name, start: currSprint.start_Date_Sprint, end: currSprint.end_Date_Sprint, color: colors[index]}
        newEvent
        this.events.push(newEvent);
      })
    }),
    )

    console.log("sprint-urile")
    // .forEach((currSprint) => {
    //     console.log("sprint-ul curent: "+currSprint)
    //     const currEvent: any = {
    //       title: currSprint., start: currSprint.start_Date_Sprint, end: currSprint.end_Date_Sprint, color: '#0000FF'
    //     }
    //     this.events.push(currEvent);
    //   }
    // )
  }

  async getSprintsFromProjects(){
    await this.getUserProject(this.username);
    console.log("all projects: ");
    console.log(this.projects);

    console.log("all sprints1: ");
    // console.log(this.projects[0].sprints);
    // console.log(this.projects[1].sprints);

    console.log("all sprints2: ");


    // this.projects.forEach((currData) => {
    // console.log("in the loop:" + this.projects.length)
    //   currData.sprints.forEach((currSprint) => {
    //     console.log("sprint from project: ");
    //     console.log(currSprint);
    //   })
    // })
  }

  private getUserProject(username: string){
    // console.log(username)
    // const user: any = this.userService.getUserByUsername(username)

    this.userService
      .getUserByUsername(username)
      .pipe(
        map((user) =>
          user.projects.sort(
            (a, b) =>
              new Date(b.start_Date_Project).getTime() -
              new Date(a.start_Date_Project).getTime()
          )
        )
      )
      .subscribe((data) => {
        data.forEach((currData) => {
          this.projects.push(currData);
          // console.log(this.projects);
        })
      })
  }

  private addAllSprints(username: string){
    // this.getUserProject(username);
    // this.getSprintsFromProjects();

    console.log(this.projects)

  }

  private getUserSprints(projectId: number) {
    this.sprintService
      .getByProjectId(projectId)
      // .subscribe((data) => (this.sprints = data));
      .subscribe((data) => {
        data.forEach((currData) => {
          // console.log(currData);
          this.sprints.push(currData);
        })
      });
  }
}

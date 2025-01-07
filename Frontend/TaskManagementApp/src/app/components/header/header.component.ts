import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/User';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProjectService } from 'src/app/services/project.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  constructor(
    private userService1: UserService,
    private projectService: ProjectService,
    public authenticationService: AuthenticationService
  ) {}
  subscription: Subscription = new Subscription();
  user!: any;
  ngOnInit(): void {
    this.user = localStorage.getItem('username');
  }
}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './components/board/board.component';
import { LoginUserComponent } from './authentication/login/login.component';
import { RegisterUserComponent } from './authentication/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginHeaderComponent } from './authentication/login-header/login-header.component';
import { LoginMainComponent } from './authentication/login-main/login-main.component';
import { RegisterMainComponent } from './authentication/register-main/register-main.component';
import { HomeComponent } from './components/home/home.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { ProjectsviewComponent } from './components/projectsview/projectsview.component';
import { ProjectstatusComponent } from './components/projectstatus/projectstatus.component';

const routes: Routes = [
  { path: '', redirectTo: 'login-main', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'login-main', component: LoginMainComponent },
  { path: 'register-main', component: RegisterMainComponent },
  //{path:'header',component:HeaderComponent}
  { path: 'board', component: BoardComponent },
  // { path: 'home', component: HomeComponent },
  { path: 'calendar', component: CalendarComponent },
  { path: 'projects-view', component: ProjectsviewComponent },
  { path: 'project-status', component: ProjectstatusComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [
  LoginUserComponent,
  DashboardComponent,
  RegisterUserComponent,
  LoginMainComponent,
  RegisterMainComponent,
];

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { HeaderComponent } from './components/header/header.component';
import { MenuComponent } from './components/menu/menu.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { ErrorHandlerService } from './services/error-handler.service';
import { BoardComponent } from './components/board/board.component';
import { LoginHeaderComponent } from './authentication/login-header/login-header.component';
import { LoginUserComponent } from './authentication/login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './components/home/home.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AuthinterceptorService } from './services/authinterceptor.service';
import { CalendarComponent } from './components/calendar/calendar.component';
import { MatSelect, MatSelectModule } from '@angular/material/select';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProjectsviewComponent } from './components/projectsview/projectsview.component';
import { OverlayModule } from '@angular/cdk/overlay';
import { MatListModule } from '@angular/material/list';
import { FilterPipe } from './pipes/filter.pipe';
import { ProjectstatusComponent } from './components/projectstatus/projectstatus.component';
import { DatePipe } from '@angular/common';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatBadge, MatBadgeModule } from '@angular/material/badge';
FullCalendarModule.registerPlugins([dayGridPlugin, interactionPlugin]);
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MenuComponent,
    DashboardComponent,
    BoardComponent,
    HomeComponent,
    CalendarComponent,
    ProjectsviewComponent,
    FilterPipe,
    ProjectstatusComponent,
  ],
  imports: [
    MatToolbarModule,
    MatBadgeModule,
    ScrollingModule,
    OverlayModule,
    MatListModule,
    MatDatepickerModule,
    MatListModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FlexLayoutModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    NgbModule,
    DragDropModule,
    FullCalendarModule,
    MatSidenavModule,
    ModalModule.forRoot(),
    // PopupModule.forRoot(),

    RouterModule.forRoot([
      {
        path: 'authentication',
        loadChildren: () =>
          import('./authentication/authentication.module').then(
            (m) => m.AuthenticationModule
          ),
      },
    ]),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthinterceptorService,
      multi: true,
    },
    [DatePipe],
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

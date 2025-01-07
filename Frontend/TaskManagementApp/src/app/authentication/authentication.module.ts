import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterUserComponent } from '../authentication/register/register.component';
import { LoginUserComponent } from '../authentication/login/login.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BoardComponent } from '../components/board/board.component';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LoginHeaderComponent } from './login-header/login-header.component';
import { LoginMainComponent } from './login-main/login-main.component';
import { RegisterHeaderComponent } from './register-header/register-header.component';
import { RegisterMainComponent } from './register-main/register-main.component';

@NgModule({
  declarations: [
    RegisterUserComponent,
    LoginUserComponent,
    LoginHeaderComponent,
    LoginMainComponent,
    RegisterHeaderComponent,
    RegisterMainComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatFormFieldModule,
    RouterModule.forChild([
      { path: 'register', component: RegisterUserComponent },
      { path: 'login', component: LoginUserComponent },
      { path: 'login-header', component: LoginHeaderComponent },
      { path: 'login-main', component: LoginMainComponent },
      { path: 'register-main', component: RegisterMainComponent },
    ]),
  ],
})
export class AuthenticationModule {}

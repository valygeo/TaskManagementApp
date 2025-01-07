import { AuthResponseDto } from './../../models/response/authenticationResponseDto';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserDto } from '../../models/userDto';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import {
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';
import { tap } from 'rxjs';

// import {
//   FormBuilder,
//   FormControl,
//   FormGroup,
//   NgForm,
//   Validators,
//   ReactiveFormsModule
// } from '@angular/forms';
@Component({
  selector: 'app-loggin-user',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginUserComponent implements OnInit {
  private returnUrl: string = '';

  loginForm!: UntypedFormGroup;
  errorMessage: string = '';
  showError: boolean = false;
  user = new User();

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loginForm = new UntypedFormGroup({
      username: new UntypedFormControl('', [Validators.required]),
      password: new UntypedFormControl('', [Validators.required]),
      email: new UntypedFormControl('', [
        Validators.email,
        Validators.required,
      ]),
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  validateControl = (controlName: string) => {
    return (
      this.loginForm.get(controlName)?.invalid &&
      this.loginForm.get(controlName)?.touched
    );
  };
  hasError = (controlName: string, errorName: string) => {
    return this.loginForm.get(controlName)?.hasError(errorName);
  };

  loginUser = (loginFormValue) => {
    this.showError = false;
    const login = { ...loginFormValue };
    const userForAuth: UserDto = {
      username: login.username,
      email: login.email,
      password: login.password,
    };
    this.authService.loginUser('api/User/login', userForAuth).subscribe({
      next: (res: AuthResponseDto) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('username', login.username),
          this.router.navigate(['board']);
        alert('Login Succes!');
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
        alert('Login failed!');
      },
    });
  };
}

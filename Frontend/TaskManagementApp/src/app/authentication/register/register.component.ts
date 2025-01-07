import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/models/userDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import {
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { PasswordConfirmationValidatorService } from 'src/app/services/custom-validators/password-confirmation-validator.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RegistrationResponseDto } from 'src/app/models/response/registrationResponseDto';

@Component({
  selector: 'app-register-user',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterUserComponent implements OnInit {
  registerForm!: UntypedFormGroup;
  public errorMessage: string = '';
  public showError!: boolean;

  hide = true;
  hide1 = true;

  constructor(
    private authService: AuthenticationService,
    private passConfValidator: PasswordConfirmationValidatorService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.registerForm = new UntypedFormGroup({
      username: new UntypedFormControl('', [
        Validators.required,
        Validators.minLength(3),
      ]),
      password: new UntypedFormControl('', [
        Validators.required,
        Validators.minLength(5),
      ]),
      email: new UntypedFormControl('', [
        Validators.email,
        Validators.required,
        Validators.minLength(5),
      ]),
      confirmPassword: new UntypedFormControl('', [Validators.required]),
    });
    this.registerForm
      .get('confirmPassword')
      ?.setValidators([
        Validators.required,
        this.passConfValidator.validateConfirmPassword(
          this.registerForm.get('password')!
        ),
      ]);
  }
  public validateControl = (controlName: string) => {
    return (
      this.registerForm.get(controlName)?.invalid &&
      this.registerForm.get(controlName)?.touched
    );
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.get(controlName)?.hasError(errorName);
  };
  public registerUser = (registerFormValue) => {
    this.showError = false;
    const formValues = { ...registerFormValue };
    const user: UserDto = {
      username: formValues.username,
      password: formValues.password,
      email: formValues.email,
    };
    this.authService.registerUser('api/User/register', user).subscribe({
      next: (response: string) => {
        console.log(response);
        alert('Register succes!');
        this.router.navigate(['login-main']);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
        alert('Register failed!');
      },
    });
  };
}

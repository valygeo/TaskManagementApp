import { Injectable, Injector } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
@Injectable({
  providedIn: 'root',
})
export class ErrorHandlerService implements HttpInterceptor {
  constructor(private router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error instanceof HttpErrorResponse) {
          console.log(error.status);
          console.log(error.statusText);
          if (error.status === 401) {
            window.location.href = '/login-main';
          }
        }
        //let errorMessage = this.handleError(error);
        return throwError(() => new Error(error.message));
      })
    );
  }
  // constructor(private inject: Injector) {}
  // intercept(
  //   req: HttpRequest<any>,
  //   next: HttpHandler
  // ): Observable<HttpEvent<any>> {
  //   let authService = this.inject.get(AuthenticationService);
  //   let jwtToken = req.clone({
  //     setHeaders: {
  //       Authorization: 'bearer' + authService.GetToken(),
  //     },
  //   });
  //   return next.handle(jwtToken);
  // }

  // private handleError = (error: HttpErrorResponse): string => {
  //   if(error.status === 404) {
  //     return this.handleNotFound(error);
  //   }
  //   else if(error.status === 400){
  //     return this.handleBadRequest(error);
  //   }
  //   return error.message;
  // }

  // private handleNotFound = (error: HttpErrorResponse): string => {
  //   //this.router.navigate(['/404']); pentru a trece la pagina de 404 not found
  //   return error.message;
  // }

  // private handleBadRequest = (error: HttpErrorResponse): string => {
  //   if(this.router.url === '/User/register'){
  //     let message = '';
  //     const values = Object.values(error.error.errors);
  //     values.map((m: string) => {
  //       message += m + '<br>';
  //       return message;
  //     })

  //     return message.slice(0, -4);
  //   }
  //   else{
  //     return error.error ? error.error: error.message;
  //   }
  // }
}

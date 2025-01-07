import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthinterceptorService implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const jwtToken = localStorage.getItem('token');

    if (jwtToken) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + jwtToken),
      });

      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}

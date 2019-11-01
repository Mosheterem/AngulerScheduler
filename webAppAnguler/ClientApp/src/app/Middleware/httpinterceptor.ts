import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from '../Services/user.service';

@Injectable()
export class JWTIntereptorService implements HttpInterceptor{

    constructor(public user: UserService) {
        
    }


  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //debugger;
    
    let token   = localStorage.getItem('AuthToken');
    if (token!=null && token !="") {
      
            request = request.clone({
                setHeaders: { 
                  Authentication: `Bearer:${token}`
                }
            });
    }
    
    return next.handle(request);
}
}

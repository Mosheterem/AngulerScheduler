import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../Services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {


  constructor(public userservice: UserService,public router: Router) { }

  canActivate(){
//debugger
    if(!this.userservice.IsUserLogin()){
      this.router.navigateByUrl('/public/login');
      return false;
    }
    return true;
  }

}

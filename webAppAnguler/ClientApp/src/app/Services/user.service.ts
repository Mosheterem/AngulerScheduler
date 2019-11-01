import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user'; 
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { HttpDataService } from './http-data.service';
import { Observable } from 'rxjs/Observable';
import { ResponseModel } from '../Models/response-model';
import { UserModal } from 'src/app/Models/usermodel';

// import { User } from '../_models/index';
@Injectable()
export class UserService {



  jwthelper: JwtHelperService;
  constructor(private httpdataservice: HttpDataService, private router: Router) {

    this.jwthelper = new JwtHelperService();
  }

  IsUserLogin() {
    if (this.isTokenExpired()) {
      localStorage.removeItem('AuthToken');
      sessionStorage.clear();
      this.router.navigateByUrl('/public/login');
      return false;
    }
    return true;

  }
  GetUserData() {
    if (this.IsUserLogin()) {
      let token = localStorage.getItem('AuthToken');
      let decodedtoken = this.jwthelper.decodeToken(token);
      let userrecord: UserModal = JSON.parse(decodedtoken.Userrecord);
      let username: string = decodedtoken.Username;
      let KeyName: string = decodedtoken.PrimeIDName;
      const userdata: any = { userrecord: userrecord, username: username };
      localStorage.setItem('KeyName', KeyName);
      return userdata;
    }
    else {
      return null
    }
  }
  isTokenExpired() {
    return this.jwthelper.isTokenExpired(localStorage.getItem('AuthToken'));
  }
  Login(data) {

    //return <Observable<ResponseModel>>this.httpdataservice.GetData("/SampleData/WeatherForecasts");
    return <Observable<ResponseModel>>this.httpdataservice.PostData("/User/login", data);
  }

  ResetPassword(email,password): any {
   
    return <Observable<ResponseModel>>this.httpdataservice.GetData("/User/resetpassword?email=" + email + "&password=" + password);
  }
  ForGotPassword(username): any {
    ;
    return <Observable<ResponseModel>>this.httpdataservice.GetData("/User/forgotpassword?Username=" + username);
  }
  //Login(data) {

  //  return <Observable<ResponseModel>>this.httpdataservice.PostData("/User/login", data);
  //}



  Barcode(data) {
    return <Observable<ResponseModel>>this.httpdataservice.PostData("/User/barcode?barcode=" + data, null)
  }

  logout() {
    localStorage.removeItem('AuthToken');
    this.router.navigateByUrl('/public/login');
  }
  SetPassword(Data): any {
    return <Observable<ResponseModel>>this.httpdataservice.PostData("/User/setpassword", Data);
  }
  ChangePassword(Data): any {
    return <Observable<ResponseModel>>this.httpdataservice.PostData("/User/changepassword", Data);
  }

  GetAllOperaters(search: string, pno) {
    return <Observable<ResponseModel>>this.httpdataservice.GetData("/Operator/getalluser?search=" + search + "&pno=" + pno);
  }
    //getAll() {
    //    return this.http.get<User[]>('/api/users');
    //}

    //getById(id: number) {
    //    return this.http.get('/api/users/' + id);
    //}

    //create(user: User) {
    //    return this.http.post('/api/users', user);
    //}

    

    

    //delete(id: number) {
    //    return this.http.delete('/api/users/' + id);
    //}
}

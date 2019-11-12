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
export class feedBackService {




  constructor(private httpdataservice: HttpDataService, private router: Router) {

    
  }

 
  AddfirstRequest(key,seckey) {
  
    // return <Observable<ResponseModel>>this.httpdataservice.GetData("feedback/AddResponse?emailid=", email, null);
    return <Observable<ResponseModel>>this.httpdataservice.GetData("/Feedback/AddResponse?key=" + key + "&secKey=" + seckey);
     
  }
 
 

  AddFeedback(data) {
    return <Observable<ResponseModel>>this.httpdataservice.PostData("/Feedback/AddFeedback",data)
  }

}

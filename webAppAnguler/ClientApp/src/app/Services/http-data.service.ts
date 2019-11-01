import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError, } from "rxjs/operators";
import { throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { CommonAlertService } from '../Helpers/common-alert.service';
import { NgxSpinnerService } from 'ngx-spinner';


@Injectable({
  providedIn: 'root'
})
export class HttpDataService {
  
  public endpointurl = environment.Url;

  constructor(private httpClient : HttpClient, private alertService: CommonAlertService,private loader:NgxSpinnerService) { 

    

  }


  GetData(url){
    return this.httpClient.get(this.endpointurl + url)
    .pipe(catchError(error=> this.handleError(error)));
  }

  PostData(url, Data){
  return this.httpClient.post(this.endpointurl+url,Data )
    .pipe(catchError(error=> this.handleError(error)));
  }

  handleError(error:HttpErrorResponse) {
    this.loader.hide()
    this.alertService.ShowErrorAlert(this.GetErrorMessage(error))
    return throwError(error);
  }
  GetErrorMessage(error: HttpErrorResponse): any {
   switch(error.status){
     case 500:{
       return "Internal Server Error";
       break;
     }
     case 401 :{
       return "Unauthorized User";
       break;
     }
     case 404 :{
       return "Not Found"
       break
     }
     default:{
      return "Internal Server Error"
     }
   }
  }

}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../Models/response-model';
import { HttpDataService } from './http-data.service';

@Injectable({
  providedIn: 'root'
})
export class AppintmentService {

  constructor(private httpdataservice: HttpDataService,) { }

  GetAppintmentByDate(data) {

    //return <Observable<ResponseModel>>this.httpdataservice.GetData("/SampleData/WeatherForecasts");
    return <Observable<ResponseModel>>this.httpdataservice.PostData("/appointment/eventByDate",data);
  }
  AddAppintments(data) {
    return <Observable<ResponseModel>>this.httpdataservice.GetData("/appointment/Addevent?values="+JSON.stringify(data));
  }
  //Getlabels

  Getlabels() {

    return <Observable<ResponseModel>>this.httpdataservice.GetData("/appointment/getlabels");
  }

  Getresources() {

    return <Observable<ResponseModel>>this.httpdataservice.GetData("/appointment/getresources");
  }
  GetUserSettings() {

    return <Observable<ResponseModel>>this.httpdataservice.GetData("/appointment/getUserSettings");
  }
  UpdateUserSettings(data) {
   
   return <Observable<ResponseModel>>this.httpdataservice.PostData("/appointment/UpdateUserSettings", data);
  }
  sendReportAsemail(data) {

    return <Observable<ResponseModel>>this.httpdataservice.PostData("/appointment/sendReportAsemail", data);
  }
 // GetLabels();
 // Getresources();
 // GetUserSettings();
//GetResources//
}

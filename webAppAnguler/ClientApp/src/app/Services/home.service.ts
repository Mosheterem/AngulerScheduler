import { Injectable } from '@angular/core';
import { HttpDataService } from './http-data.service';
import { Observable } from 'rxjs';
import { ResponseModel } from '../Models/response-model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  CurrentIndex: any;

  constructor(private http: HttpDataService) { }

  GetImageUrl(type,filename){
    return <Observable<ResponseModel>> this.http.GetData("/home/getimageurl?type="+type+"&filename="+filename);
  }
  GetBarcode(barcode){
    return <Observable<ResponseModel>> this.http.GetData("/home/getbarcode?barcode="+barcode);
  }

  GetOSSDProcessPath(index){
    //debugger;
    let OSSDConfig = sessionStorage.getItem('CSSDConig');
    if(OSSDConfig !=null && OSSDConfig !=undefined && OSSDConfig !=''){
      let indexId :any = JSON.parse(OSSDConfig)[index]
      sessionStorage.setItem('currentIndex',index);
     switch (indexId.id) {
      case 1 : {
          
        return '/private/cssdoperatorscan'
      }
      case 2: {
        return '/private/cssdmachinescan'
      }
      case 3:{
        return '/private/cssditemscan'
      }
      case 4:{
        return '/private/cssdsterilization'
      }
      default: return '/private/cssdoperatorscan'
     }
     
    }
  }


  ShowFinalLoad(index){
    let OSSDConfig = sessionStorage.getItem('CSSDConig');
    if(OSSDConfig !=null && OSSDConfig !=undefined && OSSDConfig !=''){
      let config :any = JSON.parse(OSSDConfig)[index]
      if(config.id==5){
        return true
      }
      else{
        return false;
      }
    }

  }

}

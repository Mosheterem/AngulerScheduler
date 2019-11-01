import { Injectable } from '@angular/core';
declare var $ : any

@Injectable({
  providedIn: 'root'
})
export class CommonAlertService {

  constructor() { }

  ShowSuccessAlert(message){
        console.log($);
        $("#successtext").html("").html(message);
        $("#successmessage").modal('show');
  }
  ShowErrorAlert(message){
        console.log($);
        $("#errortext").html("").html(message);
        $("#errormessage").modal('show');
  }
  ShowWarningAlert(message){

    $("#warningtext").html("").html(message);
    $("#warnmessage").modal('show');
}
  

}

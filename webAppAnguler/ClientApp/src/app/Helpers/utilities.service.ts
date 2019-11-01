import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {

  constructor() { }

   AMPMtoHRS(time:String) {
    console.log(time);
    //debugger;
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM.toLowerCase() == "pm" && hours < 12) hours = hours + 12;
    if (AMPM.toLowerCase() == "am" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return (sHours +':'+sMinutes);
}


}

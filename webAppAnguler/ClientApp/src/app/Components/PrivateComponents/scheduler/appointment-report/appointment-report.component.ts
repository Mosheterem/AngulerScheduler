
import { Component, OnInit, Input, ElementRef, ViewChild} from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { retry } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { AppintmentService } from '../../../../Services/appintmentservice.service';
import { Status } from '../../../../Models/response-model';
import { CommonAlertService } from '../../../../Helpers/common-alert.service';
@Component({
  selector: 'app-appointment-report',
  templateUrl: './appointment-report.component.html',
  styleUrls: ['./appointment-report.component.css']
})
export class AppointmentReportComponent implements OnInit {
  @ViewChild('mydata')  mydata;


  // @ViewChild('newTable', {static: false}) private  newTable: ElementRef;
  public x: any;
  title = 'hello-world';

  receiverEmail: string="";
  @Input() Resources: any;
  @Input() currentUser: any;
  @Input() stepNumberDefault :number;
  stepNumber: number = 1;
  Appointments: any ;
  MinDate: Date = new Date();
  MaxDate: Date = new Date();
  userName: string = "";
  currentDate: Date = new Date();
  selectedUser: any = {};
  Iscolor: boolean = false;
  Isvalid: boolean = false;
  constructor(private http: HttpClient, private appService: AppintmentService,private alertservice: CommonAlertService, ) {
    this.currentDate = new Date();
    //debugger;

    this.stepNumber = this.stepNumberDefault;
    this.userName = "";
    if (this.receiverEmail == "") {
      this.Isvalid = true;
    }
  }
  isvalidEmail() {
    if (this.receiverEmail == "") {
      this.Isvalid = false;
    }
    else {

    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
      if (regex.test(this.receiverEmail)) {
        this.Isvalid = true;
      }
      else {
        this.Isvalid = false;
        if (this.receiverEmail == "") {
          this.Isvalid = true;
        }
      }
    }
  }
  sendReport() {
    this.x = this.mydata.nativeElement.innerHTML;
    //console.log("xxx", this.receiverEmail);
    var payload = { email: this.receiverEmail, body: this.x };
    this.alertservice.ShowSuccessAlert("דוח ארועים מיומן היכל המשפט נשלח באימייל");
    this.appService.sendReportAsemail(payload).subscribe(resp => {
     
      this.Appointments = resp;
      this.FilterReportForuser()
      
      this.stepNumber = 2


    });
  }
  //resetStep() {
  //  this.stepNumber = 1;
  //}
  ngOnInit() {
    this.stepNumber = this.stepNumberDefault;
    // debugger;Resources
    this.userName = this.currentUser
   
  }

  getforColor(color) {
   // console.log(color);

    return color;
  }
  getTextColor(color1, color2) {
    if (color1 === color2) {
      return "black";
    }
    else {
      return color1;

    }
  }
  getbackColor(color1, color2) {


    if (color1 === color2) {
      return "white";
    }
    else {
      return color1;

    }
    //dx-scheduler-appointment-content
  }
  onChange(opt: any, event) {

    let ss = this.Resources;
    opt.isCheked = event.target.checked;
    this.Resources.map((op, i) => {
      if (op.id == opt.id) {
        op.isCheked = opt.isCheked;

      }
    });

    

  }

  FilterReportForuser() {
    //console.log('appoinmemnt...', this.Appointments);
   // console.log('rrrrr...', this.Resources);

    var newdata = this.Resources.filter(x => x.isCheked == true)
  //  console.log('filter...', newdata);

    var Forfilter = [];
    newdata.forEach(element => {
      Forfilter.push(element.id)
    });
    //console.log('xxxxxxxForfilter...', Forfilter);
    this.Appointments = this.Appointments.filter(function (a) {
      let result = false;
      Forfilter.map(function (key) {
        console.log(a.ownerIds)
        if (a.ownerIds.join().indexOf(key) > -1)
          result = true;
      });
      return result;
    })
   // console.log('after appoinmemnt...', this.Appointments);
  }
  StepNevigation() {


    var _data = {
      minDate: this.MinDate,
      maxDate: this.MaxDate
    }
    if (this.stepNumber == 1) {
     
      this.getReportDate();
    }
    else {
      this.stepNumber = 1
    }
  }
  dateBox_valueChanged(e) {
    let previousValue = e.previousValue;
    let newValue = e.value;
    console.log(newValue)
    // Event handling commands go here
  }
  getUsersforReport() {
    var name = [];
    // this.Resources.forEach(element => {
    var data = this.Resources.filter(function (el) {
      return el.isCheked == true;
    });
    //  //data.text

    data.forEach(element => {
      name.push(element.text);
    });
    // return name;

    return name.join(',')

  }
  getNameByFind(ids) {
   // console.log("gsajfgskjfgdjksfghd", ids)
    try {
      var name = [];
      ids.forEach(element => {
      //  console.log(element)
        name.push(this.Resources.find(x => x.id === element).text)

       
      });
      return name.join(" , ");
    }
    catch (error) {
      return "error with record"
    }

  }

  print(): void {
    let printContents, popupWin;
    printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title></title>
          <style>
         
          table {
            page-break-inside: avoid !important;
            margin: 4px 0 4px 0;  
          }
          </style>
        </head>
    <body style="direction: rtl;" onload="window.print();window.close()">${printContents}</body>
      </html>`
    );
    popupWin.document.close();
    this.stepNumber = 1;
  }
 
  getReportDate() {

    //debugger

    // const params: URLSearchParams = new URLSearchParams();
    // params.set('minDate',this.MinDate.toDateString());
    // params.set('maxdate', this.MaxDate.toDateString());

    let payload = {

      "maxDate": this.MaxDate,
      "minDate": this.MinDate,
    }
   // let data = JSON.stringify(payload);

    this.appService.GetAppintmentByDate(payload).subscribe(resp => {
      debugger
      this.Appointments = resp;
      this.FilterReportForuser()
      
        this.stepNumber = 2

     
    });
  }
}

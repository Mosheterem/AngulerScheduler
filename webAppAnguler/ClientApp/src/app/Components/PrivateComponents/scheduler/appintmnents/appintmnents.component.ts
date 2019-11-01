
import { NgModule, Component, enableProdMode, OnInit, ViewChild } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { DxSchedulerModule, DxSchedulerComponent } from 'devextreme-angular';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { AppintmentService } from '../../../../Services/appintmentservice.service';
import { Status } from '../../../../Models/response-model';
import { UserService } from '../../../../Services/user.service';
import { AbstractControl, Validators } from '@angular/forms';
import he from '@angular/common/locales/he';
import { registerLocaleData } from '@angular/common';
import { environment } from 'src/environments/environment';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddAppointmentComponent } from '../add-appointment/add-appointment.component';
import { element } from '@angular/core/src/render3';
import { HttpDataService } from 'src/app/Services/http-data.service';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
//registerLocaleData(he);
if (!/localhost/.test(document.location.host)) {
  enableProdMode();
}
@Component({
  selector: 'app-appintmnents',
  templateUrl: './appintmnents.component.html',
  styleUrls: ['./appintmnents.component.css']
})


export class AppintmnentsComponent implements OnInit {
  @ViewChild(DxSchedulerComponent) list: DxSchedulerComponent;

  DataSource: any = {};
  getDataSource() {
    this.stepNumberDefault=1
    this.DataSource = this.list.instance.getDataSource();
   // alert(JSON.stringify(this.DataSource));
  }

  appointmentsData: any;
  //currentDate: Date = new Date(2019, 4, 23);
  owners: any = [];
  ownersFix: any = [];
  UserSettings: any = {};
  UserSettingsFull: any = {};
  LabelsData: any = {}
  test = {}
  currentUser: any = {};
  currentDate: Date = new Date();
  resources = []
  isCRUD: boolean = false;
  tempcheck: boolean = false;
  startViewAsMerged: boolean = false;
  CellDuration: number = 20;
  LoginuserName: string = "";
  clineID?: string = '';
  ShowDeleted?: string = '';
  userName: string = '';
  primeIdName?: string = localStorage.getItem('KeyName');
  showfilter?: boolean = false;
  crudAllow?: boolean = false;
  EndHour?: number = 8;
  StartHour?: number = 16;
  userSettings: any = {};
  stepNumberDefault:number  = 1;
  ngOnInit() {


    let userdata = this.userservice.GetUserData();
    if (userdata != null) {
      this.currentUser = userdata.userrecord;

     // console.log("XXX");
      console.log(this.currentUser);
    }

   // public endpointurl = environment.Url;
    var url = environment.Url + "/appointment";
    let token = localStorage.getItem('AuthToken');
    this.appointmentsData = AspNetData.createStore({
      key: 'primeryKey',
      loadUrl: url + "/events",
      insertUrl: url + "/addEvent",
      updateUrl: url + "/update",
      deleteUrl: url + "/Delete",
      onBeforeSend: function (method, ajaxOptions) {
        //debugger
        ajaxOptions.xhrFields = { withCredentials: true };
        // ajaxOptions.headers = `Bearer:${token}`
        ajaxOptions.headers = {
          Authentication: `Bearer:${token}`
        }

      },
    });

    
    

 
  }
  
 

  constructor(private httpClient: HttpClient,private appintmentService: AppintmentService, private userservice: UserService, private modalService: NgbModal) {
    //var url = "https://js.devexpress.com/Demos/Mvc/api/SchedulerData";
    //let token = localStorage.getItem('AuthToken');

    console.log(this.appointmentsData)
    appintmentService.Getlabels()
      .subscribe(
        data => {
          this.LabelsData = data.data;
          console.log(this.LabelsData);
        },
        error => {
          console.log("Rrror", error);
        }
      );

   // console.log(this.appointmentsData)
    appintmentService.Getresources()
      .subscribe(
        Data => {
          this.owners = Data.data;
         // this.ownersFix = Data.data;
          console.log("xxxx===>");
          console.log(this.owners);
          this.ownersFix = this.owners;

          this.owners = this.ownersFix.filter(x => x.isCheked === true);


        },
        error => {
          console.log("Rrror", error);
        }
      ) 


    appintmentService.GetUserSettings()
      .subscribe(
        Data => {
          this.UserSettings = Data.data;
          console.log(this.UserSettings);
          //this.owners = this.ownersFix.filter(x => x.isCheked === true);
          this.isCRUD = this.UserSettings.isCrud;
        
          this.startViewAsMerged = this.UserSettings.startViewAsMerged;
          this.CellDuration = this.UserSettings.cellDuration;
       
      
          this.ShowDeleted = this.UserSettings.isShowDeletedRecords;
       
          this.crudAllow = this.UserSettings.isCrud;
          this.EndHour = this.UserSettings.endHour;
          this.StartHour = this.UserSettings.startHour;

         
        },
        error => {
          console.log("Rrror", error);
        }
      );
    
  }
  openFormModal() {
    const modalRef = this.modalService.open(AddAppointmentComponent);

  }
 private getData(options: any, requestOptions: any) {
        let PUBLIC_KEY = 'AIzaSyBnNAISIUKe6xdhq1_rjor2rxoI3UlMY7k',
            CALENDAR_ID = 'f7jnetm22dsjc3npc2lu3buvu4@group.calendar.google.com';
        let dataUrl = [ 'https://www.googleapis.com/calendar/v3/calendars/',
                CALENDAR_ID, '/events?key=', PUBLIC_KEY].join('');

   return this.httpClient.get(dataUrl, requestOptions).toPromise().then((data: any) => data.items);
    }

  
  onAppointmentClick(e) {
    //event.cancel = true;
   
  }
  OncellClick(e) {
    //alert(JSON.stringify(e.appointmentData));
    this.openFormModal();
  }
  toggleVisibility(e) {

    this.appintmentService.UpdateUserSettings(this.UserSettings)
    this.ShowDeleted = e.target.checked;
    if (e.target.checked) {

      this.list.instance.repaint()
    } else {
      this.list.instance.repaint()
    }
  }
  onSaveSettings() {

    this.PostSettings();

  }
  MyCustomValidationFunction(option) {
    var ss = (option.value.split(',')
      .map(email => Validators.email(<AbstractControl>{ value: email }))
      .find(_ => _ !== null) === undefined);
    //debugger
    return ss;
  }
  onChange(opt: any, event) {

    let ss = this.owners;
    opt.isCheked = event.target.checked;
    this.owners.map((op, i) => {
      if (op.id == opt.id) {
        op.isCheked = opt.isCheked;

      }
    });

    this.owners = this.ownersFix.filter(x => x.isCheked === true);
    //this.PostSettings();

  }
  getbarColor(color) {
  //  console.log(color);
   
    return color;
  }

  PostSettings() {
    var DataForPost = { Settings: this.UserSettings, owners: this.ownersFix}
    this.appintmentService.UpdateUserSettings(DataForPost)
      .subscribe(
        data => {
          ////this.UserSettingsFull = data.data;
         // console.log(this.LabelsData);
        },
        error => {
          console.log("Rrror", error);
        }
      );
  }
  setAllDayEvent(e) {
    console.log(JSON.stringify(e));

  }
  getTextColor(color1, color2) {
    if (color1 === color2) {
      return "black";
    }
    else {
      return color1;

    }
  }
  resetDefault() {
    debugger;
    this.stepNumberDefault = 0;
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
  
  onAppointmentFormCreated(data) {
  
   

    //debugger
    //alert(JSON.stringify(this.owners));
    var that = this,
      form = data.form;

   let formItems = form.option("items");
   // form.option("colCountByScreen.sm", 2);
   // form.option("colCountByScreen.md", 4);
   // this.startDate = data.appointmentData.startDate;
    //console.log("Rrror", data);
     // LabelsData = this.service.Getlabels(),
      //  DeleteData =  this.service.getBooksAndMovies(),
    //form.option("showColonAfterLabel", true)
  
    // alert();//  rtlEnabled:true,
    form.option("rtlEnabled", true)
    form.option("colCount",2)
    form.option("items", [
      
      {
        label: {
          text: "נושא",

        },
        editorType: "dxTextArea",
        colSpan: 2,
      
        dataField: "teur",
        //validationRules: [{
        //  type: "required",
        //  message: "teur is required"
        //}],
        editorOptions: {

          readOnly: false

        }
      },

      {
        label: {
          text: "שם הלקוח/תיק"
        },

        editorType: "dxTextBox",
        colSpan: 2,
        dataField: "ltName",
        editorOptions: {
          
          readOnly: false

        }

      },


      {
        label: {
          text: "זמן התחלה"
        },
        dataField: "startTime",
        editorType: "dxDateBox",
        cssClass: "inputpading",
        editorOptions: {
          width: "100%",
          type: "datetime",
          readOnly: false,
          rtlEnabled: true,
          pickerType: "calendar",

        }
      },
      {
        label: {
          text: "זמן סיום"
        },
        name: "endTime",
        dataField: "endTime",
        editorType: "dxDateBox",
        editorOptions: {
          width: "100%",
          type: "datetime",
          readOnly: false,
          pickerType: "calendar",
          rtlEnabled: true,
        }
      },
      {
        label: {
          text: "סיווג"
        },
        editorType: "dxSelectBox",
        colSpan: 2,
        dataField: "label",
        editorOptions: {
          items: this.LabelsData,
          displayExpr: "text",
          valueExpr: "id",
          fieldExpr: "Label",
          allowMultiple: false,
          readOnly: false


        }
      },
      {
        label: {
          text: "מטפל"
        },
        editorType: "dxTagBox",
        colSpan: 2,
        dataField: "ownerIds",
        editorOptions: {
          dataSource: this.ownersFix,
          displayExpr: "text",
          valueExpr: "id",
          fieldExpr: "ownerIds",
          allowMultiple: true,
          readOnly: false


        }
      },
      {
        label: {
          text: "ארוע לכל היום"
        },
        dataField: "allDayEvent",
        colSpan: 2,
      
        editorType: "dxSwitch",
        editorOptions: {
        
          readOnly: false

        }
      },
      {
        label: {
          text: "תאור"
        },
        colSpan: 2,
        dataField: "remark",
        editorType: "dxTextArea",//multiline
        editorOptions: {

          readOnly: false

        }

      }
      ,
      {
        label: {
          text: "לקוח"
        },
        dataField: "lakNum",
        editorType: "dxTextBox",//multiline

        editorOptions: {

          readOnly: false

        }
      }
      ,
      {
        label: {
          text: "תיק"
        },
        dataField: "tik",
        editorType: "dxTextBox",//multiline
        editorOptions: {

          readOnly: false

        }

      }
      ,
      {
        label: {
          text: "משתתף"
        },
        //colSpan: 2,
        dataField: "withwho",
        editorType: "dxTextBox",//multiline

        editorOptions: {

          readOnly: false

        },
        //"validationRules": [{
        //  "type": "custom",
        // // "validationCallback": this.MyCustomValidationFunction,
        //  "message": "eneter currect emails with comma saprated"
        //}]
      }
      
      ,
      {
        label: {
          text: "מקום"
        },
        dataField: "place",
        editorType: "dxTextBox",//multiline
        editorOptions: {

          readOnly: false

        }

      },
      {
        label: {
          text: "מוזמנים(emails)"
        },
        colSpan: 2,
        dataField: "attendees",
        editorType: "dxTextBox",//multiline

        editorOptions: {

          readOnly: false

        }
      }

    ]);
  }
}

 



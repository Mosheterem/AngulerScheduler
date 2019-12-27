import { NgModule, CUSTOM_ELEMENTS_SCHEMA, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangepasswordComponent } from './changepassword/changepassword.component';
//import { SetpasswordComponent } from './setpassword/setpassword.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { AppintmnentsComponent } from './scheduler/appintmnents/appintmnents.component';
import { AppointmentReportComponent } from './scheduler/appointment-report/appointment-report.component';
import { AuthGuardService } from '../../Helpers/auth-guard.service';

import { DxCheckBoxModule, DxSchedulerModule, DxSwitchModule, DxNumberBoxModule, DxDateBoxModule, DxSelectBoxModule, DxTextAreaModule, DxDataGridModule } from 'devextreme-angular';
import he from '@angular/common/locales/he';
import { registerLocaleData } from '@angular/common';
import { GroupByPipe } from '../../Helpers/groupbyPipe';
import { AddAppointmentComponent } from './scheduler/add-appointment/add-appointment.component';
import { BmaComponent } from './Calculation/BCalc/bma/bma.component';
import { BcardHomeComponent } from './Bcard/bcard-home/bcard-home.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrderByPipe } from 'src/app/Helpers/orderByPipe';
//import { MainComponent } from './Bingo/Bcard/main/main.component';
//registerLocaleData(he);

const routes: Routes = [
  {path:'', redirectTo: 'home'},
  
   
  {path:'**', redirectTo:'home'}
  
  ,
  { path: "master", component: BcardHomeComponent, canActivate: [AuthGuardService] },
  { path: "appintmnents", component: AppintmnentsComponent, canActivate: [AuthGuardService] },
  { path: "bma", component: BmaComponent, canActivate: [AuthGuardService] },
  //{ path: "Bcard", component: MainComponent }
  
]                                                              

@NgModule({
  declarations: [
    HomeComponent,
    BcardHomeComponent,
    ChangepasswordComponent,
    AppointmentReportComponent,
    AppintmnentsComponent,
    GroupByPipe,
    OrderByPipe, BmaComponent,
    AddAppointmentComponent, 
    //BmaComponent, MainComponent
    //AppointmentReportComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule,
    DxSchedulerModule,
    DxCheckBoxModule,
    DxSwitchModule, DxNumberBoxModule,
     DxDateBoxModule,
    DxSchedulerModule, DxCheckBoxModule, DxSelectBoxModule,
    DxTextAreaModule,
    DxDataGridModule,
    NgbModule.forRoot(),
    
  ],
  providers: [],
  entryComponents: [AddAppointmentComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

})
export class PrivateModule { }

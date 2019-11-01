import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { PublicLayoutComponent } from './Containers/Layout/public-layout/public-layout.component';
import { PrivateLayoutComponent } from './Containers/Layout/private-layout/private-layout.component';
import { AppComponent } from './app.component';
//import { NavMenuComponent } from './nav-menu/nav-menu.component';
//import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AppRoutingModule } from './/app-routing.module';
import { HeaderComponent } from './Containers/Container/header/header.component';
import { FooterComponent } from './Containers/Container/footer/footer.component';
import { HttpDataService } from './Services/http-data.service';
import { UserService } from './Services/user.service';
import { CommonAlertService } from './Helpers/common-alert.service';
import { DatePipe } from '@angular/common';
import { UtilitiesService } from './Helpers/utilities.service';
import { AuthGuardService } from './Helpers/auth-guard.service';
import { HomeService } from './Services/home.service';
import { JWTIntereptorService } from './Middleware/httpinterceptor';
//import { AuthenticationService } from './services/authentication.service';
import { DxSchedulerModule, DxCheckBoxModule, DxDataGridModule } from 'devextreme-angular';
import he from '@angular/common/locales/he';
import { registerLocaleData } from '@angular/common';
registerLocaleData(he);
import { Component, enableProdMode } from '@angular/core';
import { Ng2LoadingSpinnerModule } from 'ng2-loading-spinner'
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { NavbarComponent } from './Containers/Container/navbar-component/navbar-component.component';
@NgModule({
  declarations: [
    AppComponent,
   // PubliccommonleftviewComponent,
    HeaderComponent,
    FooterComponent,
  //  SidebarComponent,
    PublicLayoutComponent,
    PrivateLayoutComponent,
    NavbarComponent,
  
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    Ng2LoadingSpinnerModule.forRoot({}),
    //DxSchedulerModule,
    //DxCheckBoxModule,
   // NgxSpinnerModule,
   // DataTableModule,
   // BsDatepickerModule.forRoot(),
    //TimepickerModule.forRoot(),
    DxCheckBoxModule,
    DxDataGridModule,
    AppRoutingModule
  ],
  providers: [HttpDataService, 
    UserService, CommonAlertService,
   // AuthenticationService,
 
    UtilitiesService,
    AuthGuardService,
    HomeService,
    { provide: LOCALE_ID, useValue: 'he' },
    //{ provide: LOCALE_ID, useValue: "he-IL" },
    { provide: HTTP_INTERCEPTORS, useClass: JWTIntereptorService, multi: true, useValue: "he-IL" }],
  bootstrap: [AppComponent]
})
export class AppModule { }



import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
//import { AlertService } from '../../_services/alert.service';
//import { AuthenticationService } from '../../../services/authentication.service';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { Status } from '../../../Models/response-model';
import { CommonAlertService } from '../../../Helpers/common-alert.service';
import { UserService } from '../../../Services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginFormGroup: FormGroup;
  barcodeFormGroup: FormGroup;
  loginerrormessage: string;
  loginwithbarcode: boolean = false;
  barcode: string;
  barcodeerrmessage: string;
  showSpinner: boolean = false;
  constructor(private formBuilder: FormBuilder, private userservice: UserService,
    private router: Router, private alertservice: CommonAlertService, private loader: NgxSpinnerService,
  ) {
 


    this.loginFormGroup = new FormGroup({
      Username: new FormControl(null, Validators.required),
      Password: new FormControl(null, Validators.nullValidator),
      RememberMe: new FormControl(null, Validators.nullValidator)
    });
  }

  ngOnInit() {

    this.userservice.IsUserLogin();


    this.createform();

    if (localStorage.getItem('username') != null && localStorage.getItem('username') != "") {
      this.loginFormGroup.get('Username').setValue(localStorage.getItem('username'));
      this.loginFormGroup.get('RememberMe').setValue(true);
    }

  }
  createform(): void {
    //this.loginFormGroup = this.formBuilder.group({
    //  Username: [null, Validators.required],
    //  Password: [null, Validators.required],
    //  RememberMe: [false]
    //});

    
  }
  login() {
    this.showSpinner = true;
    if (this.loginFormGroup.valid) {
     
      this.loader.show();
     
      if (this.loginFormGroup.get('RememberMe').value == true) {
        localStorage.setItem('username', this.loginFormGroup.value.Username);

      }
      else {
        localStorage.removeItem('username');
      }

    
      this.userservice.Login(this.loginFormGroup.value).subscribe(resp => {
       
        if (resp.status == Status.Success) {
          this.showSpinner = false;
          localStorage.setItem("AuthToken", resp.data);
          let details: any = this.userservice.GetUserData();

          this.router.navigateByUrl('/private/appintmnents');
          //  }


          this.loader.hide();
        }
        else {
          this.showSpinner = false;
          this.loginerrormessage = resp.message;
        }

      });
    }
    else {
     
      Object.values(this.loginFormGroup.controls).forEach((ele) => {
        ele.markAsTouched();
      })
    }
  }
  //Loginwithbarcode() {

  //  this.loginwithbarcode = !this.loginwithbarcode;
  //  this.loginFormGroup.reset();
  //  this.loginerrormessage = null;
  //  this.barcodeFormGroup.reset();
  //  this.barcodeerrmessage = null;
  //}

  //BarcodeLogin() {
  //  if (this.barcodeFormGroup.valid) {

  //    debugger;
  //    this.userservice.Barcode(this.barcodeFormGroup.value.barcode).subscribe(resp => {
  //      debugger;
  //      if (resp.status == Status.Success) {

  //        localStorage.setItem("AuthToken", resp.data);
  //        let details: any = this.userservice.GetUserData();
  //        if (details.userrecord.LastLogin == null || details.userrecord.ResetPassword == true || details.userrecord.PasswordExpired == true) {
  //          this.router.navigateByUrl('/private/setpassword');
  //        }
  //        else {
  //          if (details.userrecord.RoleId == 1 || details.userrecord.RoleId == 2) {
  //            this.router.navigateByUrl('/private');
  //          }
  //          else if (details.userrecord.RoleId == 3) {

  //            this.router.navigateByUrl('/private/cssdoperatorscan');
  //          }
  //          else if (details.userrecord.RoleId == 4) {

  //            this.router.navigateByUrl('/private/otoperatorscan');
  //          }
  //        }
  //      }
  //      else {
  //        this.barcodeerrmessage = resp.message;
  //      }
  //    });

  //  } else {
  //    Object.values(this.barcodeFormGroup.controls).forEach((ele) => {
  //      ele.markAsTouched();
  //    })
  //  }

  //}

}


//@Component({

//  templateUrl: 'login.component.html'
//})

//export class LoginComponent implements OnInit {
//  model: any = {};
//  loading = false;
//  returnUrl: string;

//  constructor(
//    private route: ActivatedRoute,
//    private router: Router,
//    private authenticationService: AuthenticationService,
//  ) { }

//  ngOnInit() {
//    // reset login status
//    this.authenticationService.logout();

//    // get return url from route parameters or default to '/'
//    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';


//  }

//  login() {
//    this.loading = true;
//    this.authenticationService.login(this.model.username, this.model.password)
//      .subscribe(
//        data => {
//          this.router.navigate(["Event"]);
//        },
//        error => {
//          // this.alertService.error(error);
//          this.loading = false;
//        });
//  }
//}


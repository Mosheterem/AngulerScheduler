import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../../../../Services/user.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Status } from '../../../../Models/response-model';
import { CommonAlertService } from '../../../../Helpers/common-alert.service';
@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  showSpinner: boolean = false;
  ResetPasswordForm: FormGroup;
  email: string;
  pass: string;
  constructor(private loader: NgxSpinnerService, private userservice: UserService, private formBuilder: FormBuilder, private route: ActivatedRoute, private alert: CommonAlertService, ) {
    //this.ResetPasswordForm = new FormGroup({
    //  confirmpassword: this.ResetPasswordForm.confirmpassword
    //});

    this.ResetPasswordForm = new FormGroup({

      Password: new FormControl(null, Validators.nullValidator),
      confirmpassword: new FormControl(null, Validators.nullValidator)
    });
  }

  ngOnInit() {

  }
  Reset() {
    this.showSpinner = true;
    this.email = this.route.snapshot.params['email'];

    debugger
    // this.userservice.ResetPassword(this.email, this.ResetPasswordForm.controls["confirmpassword"].value);
    if (this.ResetPasswordForm.controls["confirmpassword"].value == this.ResetPasswordForm.controls["Password"].value) {
      this.userservice.ResetPassword(this.email, this.ResetPasswordForm.controls["confirmpassword"].value).subscribe(resp => {
        if (resp.status == Status.Success) {
          this.showSpinner = false;
          this.alert.ShowSuccessAlert(resp.message);
          //this.router.navigateByUrl('/public/login');
        }
        else {
          this.showSpinner = false;
          this.alert.ShowErrorAlert(resp.message);
        }
      });

    }
    else {
      this.showSpinner = false;
      this.alert.ShowErrorAlert("אימות הססמא לא תקין, נא רשום שנית");
    }
  }
}

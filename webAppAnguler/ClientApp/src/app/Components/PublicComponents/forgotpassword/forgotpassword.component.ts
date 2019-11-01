import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Services/user.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Status } from '../../../Models/response-model';
import { CommonAlertService } from '../../../Helpers/common-alert.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  showSpinner: boolean = false;
  constructor(private userservice: UserService, private formBuilder: FormBuilder, private alert: CommonAlertService, private router: Router, private loader: NgxSpinnerService) { }

  ngOnInit() {
      this.forgotPasswordForm = this.formBuilder.group({
        Username : [null,Validators.required]
      })
  }
  reset() {
  this.showSpinner = true;
    if (this.forgotPasswordForm.valid) {
     
      this.userservice.ForGotPassword(this.forgotPasswordForm.value.Username).subscribe(resp=>{
        if (resp.status == Status.Success) {
          this.showSpinner = false;
          this.alert.ShowSuccessAlert(resp.message);
          this.router.navigateByUrl('/public/login');
        }  
        else {
          this.showSpinner = false;
          this.alert.ShowErrorAlert(resp.message);
        }
      });
    }
    else{
      Object.values(this.forgotPasswordForm.controls).forEach((ele)=>{
        ele.markAsTouched();
      })
      }
  }
}

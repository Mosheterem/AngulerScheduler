import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { UserService } from '../../../Services/user.service';
import { UserModal } from '../../../Models/usermodal';
import { Status } from '../../../Models/response-model';
import { CommonAlertService } from '../../../Helpers/common-alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent implements OnInit {
  ChangePasswordForm: FormGroup;
  showmismatcherror: boolean = false;
  constructor(private formBuilder: FormBuilder, private userservice: UserService, private alertservice: CommonAlertService, private router: Router) { }

  ngOnInit() {
    this.createform();
  }
  createform(): any {
    this.ChangePasswordForm = this.formBuilder.group({

      NewPassword: [null, Validators.required],
      ConfirmPassword: [null, Validators.required],
      OldPassword: [null, Validators.required]

    })

  }
  ChangePassword() {
    if (this.ChangePasswordForm.valid) {
      if (this.ChangePasswordForm.get('NewPassword').value != this.ChangePasswordForm.get('ConfirmPassword').value) {
        this.showmismatcherror = true;
      }
      else {
        //this.showmismatcherror = false;
        //let userdata: UserModal = new UserModal();
        //let udata = this.userservice.GetUserData();
        //userdata.NewPassword = this.ChangePasswordForm.value.NewPassword;
        //userdata.Password = this.ChangePasswordForm.value.OldPassword;
        //userdata.Id = udata.userrecord.Id;;
        //userdata.Username = udata.username;

        //this.userservice.ChangePassword(userdata).subscribe(resp => {
        //  if (resp.status == Status.Success) {

        //    this.alertservice.ShowSuccessAlert(resp.message);
        //    this.router.navigateByUrl("/private/home")
        //  }
        //  else {
        //    this.alertservice.ShowErrorAlert(resp.message);
        //  }
        //});

      }

    }
    else {
      Object.values(this.ChangePasswordForm.controls).forEach((ele) => {
        ele.markAsTouched();
      })
    }
  }

}

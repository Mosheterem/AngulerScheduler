import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { feedBackService } from 'src/app/Services/feedBack.service';
import { Status } from 'src/app/Models/response-model';
import { CommonAlertService } from 'src/app/Helpers/common-alert.service';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.css']
})
export class ThankyouComponent implements OnInit {
  key: string;
  seckey: string;
  registerForm: FormGroup;
  submitted = false;
  returnvalue: Number;

  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder, private _feedBackService: feedBackService, private alert: CommonAlertService ) {


    this.route.queryParams.subscribe(params => {
      this.key = params['key'];
      this.seckey = params['seckey'];
     
    });

    this._feedBackService.AddfirstRequest(this.key, this.seckey).subscribe(resp => {
      if (resp.status == Status.Success) {
        this.returnvalue = resp.data;
        //alert(resp.data);
      }
      else {
        this.alert.ShowErrorAlert(resp.message);
      }
    });  
  }
  get f() { return this.registerForm.controls; }
  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      note: ['', Validators.minLength(0)],
      id: ['',Validators.minLength(0)],
      email: ['', [Validators.required, Validators.email]]  
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    this.registerForm.controls['id'].setValue(this.returnvalue);
    this._feedBackService.AddFeedback(this.registerForm.value).subscribe(resp => {
      if (resp.status == Status.Success) {
        this.alert.ShowSuccessAlert("תודה! אנו ניצור איתך קשר בקרוב");
      }
      else {
        this.alert.ShowErrorAlert(resp.message);
      }
    });
   
  }

}

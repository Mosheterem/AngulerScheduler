import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { feedBackService } from 'src/app/Services/feedBack.service';
import { Status } from 'src/app/Models/response-model';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.css']
})
export class ThankyouComponent implements OnInit {
  email: string;
  registerForm: FormGroup;
  submitted = false;
  //datatosend = null;

  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder, private _feedBackService: feedBackService, ) {

   // console.log('Called Constructor');
    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
     
    });

    this._feedBackService.AddfirstRequest(this.email).subscribe(resp => {
      if (resp.status == Status.Success) {

        alert('well come to the page')
      }
      else {
      
      }
    });
   
    
  }
  get f() { return this.registerForm.controls; }
  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      mobile: ['', Validators.required],
      note: ['', Validators.minLength(25)],
      email: ['', [Validators.required, Validators.email]],
     
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this._feedBackService.AddFeedback(JSON.stringify(this.registerForm.value)).subscribe(resp => {
      if (resp.status == Status.Success) {

        alert('well come to the page')
      }
      else {
     
      }
    });
    //alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value))
  }

}

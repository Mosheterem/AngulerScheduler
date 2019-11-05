import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.css']
})
export class ThankyouComponent implements OnInit {
  param1: string;
  param2: string;
  constructor(private route: ActivatedRoute) {

    console.log('Called Constructor');
    this.route.queryParams.subscribe(params => {
      this.param1 = params['param1'];
      this.param2 = params['param2'];
    });
    console.log(this.param1 + "nd" + this.param2);
  }

  ngOnInit() {
alert();
  }

}

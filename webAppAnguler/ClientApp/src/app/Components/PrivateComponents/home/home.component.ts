import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})  
export class HomeComponent implements OnInit {
  name: string;

  constructor(private userservice : UserService) { }

  ngOnInit() {
   // let userdata = this.userservice.GetUserData();
    //if(userdata){
     // this.name =  userdata.userrecord.FirstName + " " + userdata.userrecord.LastName
    // }
     
  }
  
}

//import { Component, OnInit } from '@angular/core';
//import { UserService } from '../../../Services/user.service';

//@Component({
//  selector: 'app-header',
//  templateUrl: './header.component.html',
//  styleUrls: ['./header.component.css']
//})
//export class HeaderComponent implements OnInit {

//  constructor(private userservice : UserService) { }
//  name: string;
//  ngOnInit() {
//    let userdata = this.userservice.GetUserData();
//    if(userdata!=null){
//     this.name =   userdata.userrecord.FirstName + " " + userdata.userrecord.LastName
//    }
//  }


//}
import { Component, OnInit } from '@angular/core';
//import { AuthenticationService } from '../../../Services/authentication.service';
import { UserService } from '../../../Services/user.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  subscription: any;
  constructor(private userservice: UserService) { }
  name: string;
  ngOnInit() {

    let userdata = this.userservice.GetUserData();
    if (userdata != null) {
      this.name = userdata.userrecord.UserName 

    }
  }

  Logout() {
    this.userservice.logout();
  }
 
}

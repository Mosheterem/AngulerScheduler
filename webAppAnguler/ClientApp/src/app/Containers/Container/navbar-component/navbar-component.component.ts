


import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Services/user.service';

@Component({
  selector: 'app-navbar-component',
  templateUrl: './navbar-component.component.html',
  styleUrls: ['./navbar-component.component.css']
})
export class NavbarComponent implements OnInit {
  name;
  shownavbar: boolean = false;
  constructor(private userservice: UserService) { }

  ngOnInit() {
   
  }

}



import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  
  constructor(private userService : UserService, private alertify : AlertifyService) { }
  users! : User[];
  ngOnInit(): void {
    this.getUser();
  }

  getUser(){
    this.userService.getUsers().subscribe(users => {
      this.users = users;
    },err =>{
      this.alertify.error(err);
    })
  }
}

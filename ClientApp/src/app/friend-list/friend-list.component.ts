import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  users: User[] | undefined;
  followParams: string = "followings";
  loading=false;
  constructor(private userService: UserService, 
    private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.loading=true;
    this.userService.getUsers(this.followParams.toString()).subscribe(users => {
      this.loading=false;
      this.users = users;
    }, err => {
      this.loading=false;
      this.alertify.error(err);
    })
  }

}

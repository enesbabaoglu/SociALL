import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from '../../_models/user';
import { AlertifyService } from '../../_services/alertify.service';
import { UserService } from '../../_services/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  public loading = false;
  userParams: any = {};
  followText! : string  ;

  constructor(private userService : UserService, private alertify : AlertifyService,private authService : AuthService) { }
  users! : User[];
  ngOnInit(): void {
    this.getUser();
    
  }

  getUser(){
    this.loading=true;
    this.userService.getUsers(null, this.userParams).subscribe(users => {
      this.loading=false;
      this.users = users;
    },err =>{
      this.loading=false;
      this.alertify.error(err);
    })
  }
  isFollowedUser(userId: any){
   let text : string ;
   var subject = new Subject<string>();
    this.userService.isFollowedUser(this.authService.decodedToken.nameid, userId).subscribe(result => {
      if(result) {
        text= "Takip ediliyor" ;
      }
      else{
        text="Takip Et";
      } 
      subject.next(text);
    }, err => {
       this.alertify.error(err);
    });
    return subject.pipe();
  }
}

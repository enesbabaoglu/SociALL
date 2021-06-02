import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {

  user!: User;
  followText! : string  ;

  constructor(private userService: UserService, private alertify: AlertifyService,
    private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    this.getUser();
    this.isFollowedUser(this.route.snapshot.params['id']);
  }
  getUser() {
    this.userService.getUser(+this.route.snapshot.params['id']).subscribe(user => {
      this.user = user;
    }, err => {
      this.alertify.error(err);
    })
  }

  followUser(userId: any) {

    this.userService.followUser(this.authService.decodedToken.nameid, userId).subscribe(result => {
      this.isFollowedUser(userId);
    }, err => {
      this.alertify.error(err);
    })
  }

   isFollowedUser(userId: any) {

    this.userService.isFollowedUser(this.authService.decodedToken.nameid, userId).subscribe(result => {
      result ? this.followText = "Takip ediliyor" : this.followText = "Takip Et";
    }, err => {
      this.alertify.error(err);
    })
  }

}

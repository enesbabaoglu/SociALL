import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  user: User = new User;

  constructor(private route: ActivatedRoute,private userService : UserService,
     private authService : AuthService ,
      private alertify : AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=> {
      this.user = data.user;
    })
  }

  updateUser(){
    this.userService.updateUser(this.authService.decodedToken.nameid,this.user).subscribe(result => {
      this.alertify.success("Güncelleme Başarılı :)");
    },err => {
      this.alertify.error ("Güncelleme hatası : " + err);
    });
  }
}
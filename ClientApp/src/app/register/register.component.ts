import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  constructor(private authService : AuthService,
    private alertify : AlertifyService,
    private route :Router) { }

  ngOnInit(): void {
  }

  register(){
    this.authService.register(this.model).subscribe(result => {
      this.alertify.success("Kayıt Başarılı :)");
      this.authService.login(this.model).subscribe(result => {
        this.route.navigate(["/members"]);
      })
    },error => {
      this.alertify.error("Kayıt Başarısız : " + error);
    });
  }
}

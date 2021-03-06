import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model : any = {};
  isActive : boolean = false;
  constructor(public authService : AuthService, private router : Router,private alertify : AlertifyService) { }

  ngOnInit(): void {
  }
  login(){
    this.authService.login(this.model).subscribe(next=> {
      this.alertify.success("login başarılı");
      this.router.navigate(['/members']);
    },error => {
      this.alertify.error(error);
    });
    
    
  }
   loggedIn() : boolean{
    return this.authService.loggedIn();
   }
   logout(){
     localStorage.removeItem("token");
     this.router.navigate(['/home']);
   }
}

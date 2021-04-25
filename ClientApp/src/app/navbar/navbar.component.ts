import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model : any = {};
  isActive : boolean = false;
  constructor(private authService : AuthService, private router : Router) { }

  ngOnInit(): void {
  }
  login(){
    this.authService.login(this.model).subscribe(next=> {
      console.log("login başarılı");
      this.router.navigate(['/members']);
    },error => {
      console.log("login hatalı");
    });
    
    
  }
   loggedIn() : boolean{
    return localStorage.getItem("token")?true:false;
   }
   logout(){
     localStorage.removeItem("token");
     this.router.navigate(['/home']);
   }
}

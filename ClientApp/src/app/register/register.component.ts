import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  constructor(private authService : AuthService) { }

  ngOnInit(): void {
  }

  register(){
    this.authService.register(this.model).subscribe(result => {
      console.log("Okey");
    },error => {
      console.log("hata");
    });
  }
}

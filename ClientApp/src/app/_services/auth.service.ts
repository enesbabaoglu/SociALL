import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators'
import { JwtHelperService } from "@auth0/angular-jwt";
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl : string = "http://localhost:5000/api/Auth/";
  jwtHelper= new JwtHelperService();
  decodedToken : any;

  constructor(private http:HttpClient) { }

  login(model:User){
    return this.http.post(this.baseUrl+'login',model).pipe(
      map((response:any) => {
        const result=response
        if(result){
          localStorage.setItem("token",result.token)
        }
      })
    )
  }

  tokenType  = 'Bearer ';
  register(model: any){
    return this.http.post(this.baseUrl + 'register',model);
  }
   public static  getToken() {
    return localStorage.getItem("token")?.toString();
  }

  loggedIn(){
   return !this.jwtHelper.isTokenExpired(AuthService.getToken());
  }
}

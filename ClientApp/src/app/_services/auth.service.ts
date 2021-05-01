import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators'
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl : string = "http://localhost:5000/api/User/";
  jwtHelper= new JwtHelperService();
  decodedToken : any;

  constructor(private http:HttpClient) { }

  login(model:any){
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
    const header = new HttpHeaders().set('Authorization', this.tokenType + localStorage.getItem('token'));
    const headers = { headers: header };
    return this.http.post(this.baseUrl+'register',model,headers).pipe(
      map((response:any)=> {
        const result=response
        if(result){
          console.log("Kayıt başarılı.")
        }
      })
    )
  }

  loggedIn(){
   return !this.jwtHelper.isTokenExpired(localStorage.getItem("token")?.toString());
  }
}

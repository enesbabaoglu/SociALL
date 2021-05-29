import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { empty, EmptyError, Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { User } from "../_models/user";
import { AlertifyService } from "../_services/alertify.service";
import { AuthService } from "../_services/auth.service";
import { UserService } from "../_services/user.service";

@Injectable()

export class MemberEditResolver implements Resolve<User> {

    /**
     *
     */
    constructor(private AuthService : AuthService,
        private UserService : UserService,
        private alertifyService : AlertifyService,
        private Route : Router) {

    }


    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        return this.UserService.getUser(this.AuthService.decodedToken.nameid).pipe(catchError(error => {
            this.alertifyService.error("server error");
            this.Route.navigate(['/members']);
            return EmptyError;
        }))
    }


}


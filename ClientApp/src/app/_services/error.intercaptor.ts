import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";


export class ErrorIntercaptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(req).pipe(catchError((error: HttpErrorResponse) => {
            if (error.status === 400) {
                if(error.error.errors){
                    console.log(error);
                    
                    const serverError = error.error;
                    let errorMessage='';

                    for(const key in serverError.errors){
                        errorMessage+= serverError.errors[key]+'\n';
                    }
                    console.log(errorMessage);
                    return throwError(errorMessage)
                }
            }
            else if (error.status === 401) {
            
                return throwError(error.statusText);
             }
             return throwError(error.error.Message);
        }))
    }

}
import { inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpInterceptorFn
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { NavigationErrorHandlerFeature, NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
   const router = inject(Router);
   const toastr = inject(ToastrService);

   return next(req).pipe(
    catchError(error => {
      if(error){
        switch(error.status){
          case 400:
            if(error.error.errors){
              const modalStateErrors = []
              for(const key in error.error.errors){
                if(error.error.errors[key]){
                  modalStateErrors.push(error.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            }
            else{
              toastr.error(error.error, error.status);
            }
            break;
          case 401:
            toastr.error("Unauthorised", error.status);
            break;
          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: error.error}};
            router.navigateByUrl('/server-error', navigationExtras);
            break;
            default :
              toastr.error('Somthing unexpectedwent wrong');
              break;
        }
      }
      throw error;
    })
   );
}

// @Injectable()
// export class ErrorInterceptor implements HttpInterceptor {

//   router = inject(Router);
//   toastr = inject(ToastrService);


//   constructor() {}

//   intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
//     return next.handle(request);
//   }
// }

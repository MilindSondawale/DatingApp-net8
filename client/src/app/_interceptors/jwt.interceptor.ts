import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountServer=inject(AccountService);

  if(accountServer.currentUser() ){
    req=req.clone({
      setHeaders:{
        Authorization:`Bearer ${accountServer.currentUser()?.token}`
      }
    })
  }
  return next(req);
};

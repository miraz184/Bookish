import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, Route } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './services/authentication/authentication.service';
import { RouterService } from './services/routerService/router.service';



@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  constructor(private authService: AuthenticationService, private router: RouterService, private routerService: RouterService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    const bearerToken = this.authService.getBearerToken();
    const authenticationPromise = this.authService.isUserAuthenticated(bearerToken);
    return authenticationPromise.then((isAuthenticated) => {
      if (!isAuthenticated) {
                this.routerService.routeToLogin();
      }
      return isAuthenticated;
    });
  }
}

  


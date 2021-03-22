import { Location } from '@angular/common';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouterService {

  constructor(private router:Router, private location:Location) { }

  routeToDashboard() {
    this.router.navigate(['dashboard']);
  }

  routeToLogin() {
    this.router.navigate(['/login']);
  }
  routeToSearch(){
    this.router.navigate(['book-search']);
  }
  routeToFavourite(){
    this.router.navigate(['/fav-list']);
  }

  routeBack() {
    this.location.back();
  }


}

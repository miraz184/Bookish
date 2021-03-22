import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { RouterService } from '../services/routerService/router.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private authService:AuthenticationService , private router : RouterService) { }

  ngOnInit(): void {
    this.authService.logOutUser();
    this.router.routeToLogin();
  }


}

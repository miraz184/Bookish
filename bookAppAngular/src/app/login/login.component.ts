import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { RouterService } from '../services/routerService/router.service';
import { User } from '../user';
import { LoginUser } from './login-user';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  user: LoginUser;
  submitMessage: string;
  constructor(private formBuilder: FormBuilder,
    private routerService: RouterService,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
    ) {
      this.submitMessage = '';
    
  }
  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
    
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }
 
    onSubmit() {
       this.authenticationService.authenticateUser(this.f.username.value, this.f.password.value)
    
    .subscribe(
      res => {
        this.authenticationService.setBearerToken(res['token']);
        localStorage.setItem('username',this.f.username.value);
        this.routerService.routeToSearch();
      },
      err => {
        if (err.status === 403) {
          this.submitMessage = err.error.message;
        } else if (err.status === 0) {
          this.submitMessage = '';
        } else {
          this.submitMessage = err.message;
        }
      }
    );
  }
}

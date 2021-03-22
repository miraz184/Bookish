import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterUser } from 'src/app/regsiter/register-user';
import { User } from 'src/app/user';



@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  // private currentUserSubject: BehaviorSubject<User>;
  // public currentUser: Observable<User>;

  dotNetEndPoint: string;
  token: string;
 // http://localhost:3000/api/v1/notes
  constructor(private httpClient: HttpClient) {
    this.dotNetEndPoint = '"http://localhost:8090/api/Auth/isAuthenticated'
    // this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    // this.currentUser = this.currentUserSubject.asObservable();
  }
  

//   public get currentUserValue(): User {
//     return this.currentUserSubject.value;
// }

  
  // http://localhost:50110/api/Auth/login
  // login(username: string, password: string) {
  //   return this.httpClient.post<any>(`http://localhost:50110/api/Auth/login/`, { username, password })
  //       .pipe(map(user => {
  //           // login successful if there's a jwt token in the response
  //           if (user && user.token) {
  //               // store user details and jwt token in local storage to keep user logged in between page refreshes
  //               localStorage.setItem('currentUser', JSON.stringify(user));
  //               this.currentUserSubject.next(user);
  //           }
  //           return user;
  //    }));
  // }
 
 
  // logout() {
  //   // remove user from local storage to log user out
  //   localStorage.removeItem('bearertoken');
  //   this.currentUserSubject.next(null);
  // }
  getBearerToken(){
    return localStorage.getItem('bearertoken');
  }
  setBearerToken(token) {
    localStorage.setItem('bearertoken',token);
  }

  deleteToken() {
    return localStorage.removeItem('bearertoken');
  }

  isUserAuthenticated(token): Promise<boolean> {
    return this.httpClient.post(`${this.dotNetEndPoint}isAuthenticated`, {}, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${token}`)
    }).pipe(map(reponse => reponse['isAuthenticated'])).toPromise();
  }

  authenticateUser(username: string, password: string){
  
  return this.httpClient.post('http://localhost:8090/api/Auth/login',{ username, password });
    
  }
  logOutUser() {
    localStorage.removeItem('bearertoken');
    
  }


}

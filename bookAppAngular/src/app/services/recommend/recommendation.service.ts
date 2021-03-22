import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/app/book';
import { AuthenticationService } from '../authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {
  book:Book;
  constructor(private httpClient: HttpClient,private authService:AuthenticationService) { }

  public getRecommendation():Observable<any>{
   return this.httpClient.get("http://localhost:8087/api/Recommend/getRecommends",{
    headers: new HttpHeaders().set('Authorization',`Bearer ${this.authService.getBearerToken()}`)
   });
  }
  
}

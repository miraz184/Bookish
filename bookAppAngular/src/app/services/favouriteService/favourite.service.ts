import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Book } from 'src/app/book';
import { LoginUser } from 'src/app/login/login-user';
import { User } from 'src/app/user';
import { AuthenticationService } from '../authentication/authentication.service';
import { tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  book: Book;
  books: Array<Book>;
  bookSubject: BehaviorSubject<Array<Book>>;
  private bearerToken: string;

  constructor(private httpClient: HttpClient, private authService: AuthenticationService) {
    this.book = new Book()
  }

  addToFavourite(book: Book): Observable<Book> {
    book.CreatedBy = localStorage.getItem('username');
    return this.httpClient.post<Book>(`http://localhost:8088/api/Favourite/`, book, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.authService.getBearerToken()}`)
    }).pipe(tap((res: any) => { this.books.push(res); }));
  }


  removeFavourite(novel: string, user: string): Observable<Book> {
    user1: User;
    return this.httpClient.delete(`http://localhost:8088/api/Favourite/${novel}/${user}`,
      { headers: new HttpHeaders().set('Authorization', `Bearer ${this.authService.getBearerToken()}`) })
      .pipe(tap((res: any) => {
        this.books.push(res);
      }));
  }

  getAllFavouritesByUserId(user: string): Observable<any> {
    return this.httpClient.get(`http://localhost:8088/api/Favourite/${user}`,
      { headers: new HttpHeaders().set('Authorization', `Bearer ${this.authService.getBearerToken()}`) })
      .pipe(tap((res: any) => { this.books = res; 
        // window.location.reload();
        // this.bookSubject.next(this.books)
      }));

  }

}
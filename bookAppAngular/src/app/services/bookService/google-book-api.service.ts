import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class GoogleBookApiService {

  constructor(private httpClient: HttpClient) { }

  
  SearchBooks(search) {
    const encodedURI  = encodeURI("https://www.googleapis.com/books/v1/volumes?q="+ search +"&maxResults=12")
    return this.httpClient.get(encodedURI)
      //  .pipe(map((response: Response) => response.json()))
  };

  SearchByISBN(isbn){
  var encodedURI = encodeURI("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn +"&maxResults=1");
  return this.httpClient.get(encodedURI)
    .pipe(map((response: Response) => response.json()));
}
}

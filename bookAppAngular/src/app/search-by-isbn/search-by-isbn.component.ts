import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GoogleBookApiService } from '../services/bookService/google-book-api.service';

@Component({
  selector: 'app-search-by-isbn',
  templateUrl: './search-by-isbn.component.html',
  styleUrls: ['./search-by-isbn.component.css']
})
export class SearchByIsbnComponent implements OnInit {
  book;
  constructor(private googleBookApiService:GoogleBookApiService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let isbn = this.route.snapshot.params['isbn'];
     
    this.googleBookApiService.SearchByISBN(isbn).
         subscribe((data) => {
            
            this.book=data[0];
    }); 

  }

}

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../book';
import { LoginUser } from '../login/login-user';
import { GoogleBookApiService } from '../services/bookService/google-book-api.service';
import { FavouriteService } from '../services/favouriteService/favourite.service';
import { RecommendationService } from '../services/recommend/recommendation.service';
import { RouterService } from '../services/routerService/router.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  books1: Book;
  books: Book[] = [];

  recommendedBooks;
  user: LoginUser;
  constructor(private googleBookApi: GoogleBookApiService, private favService: FavouriteService,
    private recommendService: RecommendationService, private routeService: RouterService) {
    this.books1 = new Book();
  }
  OnSearch(find) {
    this.googleBookApi.SearchBooks(find)
      .subscribe((data: any) => {
        this.books = data.items;
      });
  }
  ngOnInit(): void {
    this.recommendService.getRecommendation().subscribe(res => {
      this.recommendedBooks = res;
    })
  }
  addFavourite(bookValue) {
   
    this.books1.BookId = bookValue.id;
    this.books1.Author = bookValue.volumeInfo.authors[0];
    this.books1.Title = bookValue.volumeInfo.title;
    this.books1.Category = bookValue.volumeInfo.categories[0];
    this.books1.CreatedBy = localStorage.getItem('username');
    this.favService.addToFavourite(this.books1).subscribe(res => {
      this.books.push(res);
      this.routeService.routeToFavourite();
    }, e => { e.message })

  }

}

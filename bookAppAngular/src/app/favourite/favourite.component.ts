import { Component, OnInit } from '@angular/core';
import { Book } from '../book';
import { LoginUser } from '../login/login-user';
import { FavouriteService } from '../services/favouriteService/favourite.service';
import { User } from '../user';

@Component({
  selector: 'app-favourite',
  templateUrl: './favourite.component.html',
  styleUrls: ['./favourite.component.css']
})
export class FavouriteComponent implements OnInit {
  book:Book;
  user:User;
  result:any;
  username:string;
  books:Book[]=[];
  errorMessage:string;

  constructor(private favouriteService: FavouriteService) {
    this.book=new Book();
    this.user=new User();
    this.username='';
   }

  ngOnInit(): void {
    this.username=localStorage.getItem('username');
    this.book.CreatedBy=this.username;
    this.favouriteService.getAllFavouritesByUserId(this.book.CreatedBy).subscribe(res=>{
      this.result=res;
      
    }),e=>{this.errorMessage=e.message; 
    };

  }
  removeFromFavourote(bookId,userId){
   

      this.favouriteService.removeFavourite(bookId,userId).subscribe(
        
        res=>{
         
          this.book=res;
         
           
        },e=>{this.errorMessage=e.message;})
        window.location.reload();
        }
  }
 
import { Component, OnInit } from '@angular/core';
import { GoogleBookApiService } from '../services/bookService/google-book-api.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private googleBookApi: GoogleBookApiService) {
   
  }


  ngOnInit(): void {
  }

}

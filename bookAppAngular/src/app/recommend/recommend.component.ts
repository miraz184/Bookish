import { Component, OnInit } from '@angular/core';
import { Book } from '../book';
import { RecommendationService } from '../services/recommend/recommendation.service';

@Component({
  selector: 'app-recommend',
  templateUrl: './recommend.component.html',
  styleUrls: ['./recommend.component.css']
})
export class RecommendComponent implements OnInit {
books:Book[]=[];
book:Book;
errMessage:string
  constructor(private recommService:RecommendationService) { }

  ngOnInit(): void {
    this.recommService.getRecommendation().subscribe(res=>{
      this.books=res,
      err=>this.errMessage=err.message});
    }
    
  }


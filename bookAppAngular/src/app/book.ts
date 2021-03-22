export class Book {
  BookId:string;
  Title:string;
  Author:string;
  Category:string;
  CreatedBy?:string;

    constructor() {
      this.BookId='';
        this.Title = '';
        this.Author = '';
        this.Category = '';
      }
    
}

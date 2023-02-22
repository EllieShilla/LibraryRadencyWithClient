import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';
import { BasicInfoAboutBookWithReview } from '../models/basicInfoAboutBookWithReviews';
import { IBasicInfoAboutBook } from '../models/basicInfoAboutBook';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class BookListService {
  baseUrl = environment.baseUrl;
  books: IBasicInfoAboutBook[] = [];
  book: BasicInfoAboutBookWithReview = new BasicInfoAboutBookWithReview();

  constructor(private http: HttpClient) {}

  getAllBooks() {
    return this.http
      .get<IBasicInfoAboutBook[]>(this.baseUrl + 'books/all')
      .pipe(
        map((response) => {
          this.books = response;
          return this.books;
        })
      );
  }

  getTo10Books() {
    return this.http
      .get<IBasicInfoAboutBook[]>(this.baseUrl + 'recommended')
      .pipe(
        map((response) => {
          this.books = response;
          return this.books;
        })
      );
  }

  getTop10BooksByGenre(genre: string) {
    let params = new HttpParams();
    params = params.append('genre', genre);

    return this.http
      .get<IBasicInfoAboutBook[]>(this.baseUrl + 'recommended', { params })
      .pipe(
        map((response) => {
          this.books = response;
          return this.books;
        })
      );
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { BasicInfoAboutBookWithReview } from '../models/basicInfoAboutBookWithReviews';

@Injectable({
  providedIn: 'root',
})
export class BookListItemService {
  baseUrl = environment.baseUrl;
  book!: BasicInfoAboutBookWithReview;
  private messageSource = new BehaviorSubject('');
  currentMessage = this.messageSource.asObservable();

  constructor(private http: HttpClient) {}

  getBookById(id: number) {
    return this.http
      .get<BasicInfoAboutBookWithReview>(this.baseUrl + 'books/' + id)
      .pipe(
        map((response) => {
          this.book = response;
          return this.book;
        })
      );
  }
}

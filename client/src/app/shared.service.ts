import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BasicInfoAboutBookWithReview } from './models/basicInfoAboutBookWithReviews';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  constructor() {}

  public id$ = new Subject<number>();
  public idForShow$ = new Subject<number>();
  public book$ = new Subject<BasicInfoAboutBookWithReview>();
  public isUpdate$=new Subject<boolean>();

  public sendId(id: number) {
    this.id$.next(id);
  }

  public sendBookForUpdate(book: BasicInfoAboutBookWithReview) {
    this.book$.next(book);
  }

  public sendBookForModal(id: number) {
    this.idForShow$.next(id);
  }

  public updateAllBooks(isUpdate: boolean) {
    this.isUpdate$.next(isUpdate);
  }
}

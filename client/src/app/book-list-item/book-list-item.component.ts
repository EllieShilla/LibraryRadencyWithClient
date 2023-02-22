import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { BasicInfoAboutBookWithReview } from '../models/basicInfoAboutBookWithReviews';
import { SharedService } from '../shared.service';
import { BookListItemService } from './book-list-item.service';
import { ViewBookComponent } from './view-book/view-book.component';

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.component.html',
  styleUrls: ['./book-list-item.component.css'],
})
export class BookListItemComponent implements OnInit {
  bookId!: number;
  book: BasicInfoAboutBookWithReview = new BasicInfoAboutBookWithReview();
  clickEventsubscription!: Subscription;
  modalRef!: BsModalRef;

  constructor(
    private modalService: BsModalService,
    private bookListItemService: BookListItemService,
    private sharedService: SharedService
  ) {
    this.sharedService.id$.subscribe((id) => this.getBookById(id, 'update'));
    this.sharedService.idForShow$.subscribe((idForShow) =>
      this.getBookById(idForShow, 'show')
    );
  }

  ngOnInit(): void {}

  getBookById(id: number, whatToDo: string) {
    this.bookListItemService.getBookById(id).subscribe((response) => {
      switch (whatToDo) {
        case 'show':
          this.openModal(response);
          break;
        case 'update':
          this.sharedService.sendBookForUpdate(response);
          break;
      }
    },
    (error) => {
      console.log(error);
    });
  }

  openModal(book: BasicInfoAboutBookWithReview) {
    this.modalRef = this.modalService.show(ViewBookComponent, {
      class: 'modal-lg',
      initialState: {
        book: book,
      },
    });
  }
}

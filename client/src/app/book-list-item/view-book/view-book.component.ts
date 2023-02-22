import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BasicInfoAboutBookWithReview } from 'src/app/models/basicInfoAboutBookWithReviews';

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.css'],
})
export class ViewBookComponent {
  book: BasicInfoAboutBookWithReview = new BasicInfoAboutBookWithReview();

  constructor(private modalService: BsModalService) {}

  onClose() {
    this.modalService.hide();
  }
}

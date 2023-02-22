import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { EditBookComponent } from '../edit-book/edit-book.component';
import { IBasicInfoAboutBook } from '../models/basicInfoAboutBook';
import { SharedService } from '../shared.service';
import { BookListService } from './book-list.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
})
export class BookListComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTearm?: ElementRef;
  @Input() editBookComponent!: EditBookComponent;

  books: IBasicInfoAboutBook[] = [];
  constructor(
    private bookListService: BookListService,
    private sharedService: SharedService) {
    this.sharedService.isUpdate$.subscribe(() => this.changeColor(1));
  }

  ngOnInit(): void {
    this.changeColor(1);
    this.getBooks();
  }

  getBooks() {
    this.bookListService.getAllBooks().subscribe((response) => {
      this.books = response;
    },
    (error) => {
      console.log(error);
    });
  }

  getTo10Books() {
    this.bookListService.getTo10Books().subscribe((response) => {
      this.books = response;
    },
    (error) => {
      console.log(error);
    });
  }

  getTop10BooksByGenre() {
    this.bookListService
      .getTop10BooksByGenre(this.searchTearm?.nativeElement.value)
      .subscribe((response) => {
        this.books = response;
      },
      (error) => {
        console.log(error);
      });
  }

  changeColor(num: number) {
    var allBtn = document.getElementById('allBtn');
    var recBtn = document.getElementById('recBtn');
    var genreForRec = document.getElementById('genreForRec');
    var genreForRecBtn = document.getElementById('genreForRecBtn');

    if (
      allBtn != null &&
      recBtn != null &&
      genreForRec != null &&
      genreForRecBtn != null
    ) {
      if (num == 1) {
        allBtn.className = 'btn btn-warning';
        recBtn.className = 'btn btn-primary';
        genreForRec.style.visibility = 'hidden';
        genreForRecBtn.style.visibility = 'hidden';
        this.getBooks();
      } else {
        recBtn.className = 'btn btn-warning';
        allBtn.className = 'btn btn-primary';
        genreForRec.style.visibility = 'visible';
        genreForRecBtn.style.visibility = 'visible';
        this.getTo10Books();
      }
    }
  }

  openModal(id: number) {
    this.sharedService.sendBookForModal(id);
  }

  updateBook(num: number) {
    this.editBookComponent.title = 'Edit';
    this.sharedService.sendId(num);
  }
}

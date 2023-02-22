import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BasicInfoAboutBookWithReview } from '../models/basicInfoAboutBookWithReviews';
import { SharedService } from '../shared.service';
import { EditBookService } from './edit-book.service';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css'],
})
export class EditBookComponent implements OnInit {
  bookForm!: FormGroup;
  cover!: string;
  title: string = 'Add';
  bookId!: number;
  isUpdate: boolean = false;

  constructor(
    private fb: FormBuilder,
    private editBookService: EditBookService,
    private sharedService: SharedService
  ) {
    this.sharedService.book$.subscribe((book) => this.formReset(book));
  }
  ngOnInit(): void {
    this.createBookForm();
  }

  createBookForm() {
    this.bookForm = this.fb.group({
      id: [0],
      title: [null, Validators.required],
      cover: [null, Validators.required],
      content: [null, Validators.required],
      genre: [null, Validators.required],
      author: [null, Validators.required],
    });
  }

  picked(event: any) {
    let fileName = <HTMLInputElement>document.getElementById('fileName');
    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = (e: any) => {
      const imgBase64Path = e.target.result;
      fileName.value = file.name.toString();
      this.bookForm.patchValue({ cover: imgBase64Path.toString() });
    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
  }

  onSubmit() {
    if (this.bookForm.valid) {
      if (this.bookForm.controls['id'].value == null)
        this.bookForm.patchValue({ id: 0 });

      this.editBookService.newBook(this.bookForm.value).subscribe(
        (response) => {
          this.sharedService.updateAllBooks(true);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  onReset() {
    this.bookForm.reset('');
    this.title = 'Add';
  }

  formReset(book: BasicInfoAboutBookWithReview) {
    this.bookForm.reset(book);
  }
}

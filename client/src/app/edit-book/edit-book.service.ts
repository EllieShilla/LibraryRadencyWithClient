import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class EditBookService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) {}

  newBook(value: any) {
    return this.http.post(this.baseUrl + 'books/save', value).pipe(
      map((id) => {
        return id;
      })
    );
  }
}

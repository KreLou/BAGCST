import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NewsItem } from 'src/app/models/NewsItem';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsFeedLoaderService {

  constructor(private http: HttpClient) { }

  load(start = 0, amount = 10): Observable<NewsItem[]> {
    return this.http.get<NewsItem[]>(environment.apiURL + `/api/news?start=${start}&amount=${amount}`);
  }
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NewsItem } from 'src/app/models/NewsItem';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PostGroup } from 'src/app/models/PostGroup';
import { GlobalHTTPService } from './global-http.service';

@Injectable({
  providedIn: 'root'
})
export class NewsFeedLoaderService {

  constructor(private http: HttpClient, private httpGlobal: GlobalHTTPService) { }

  load(start = 0, amount = 10): Observable<NewsItem[]> {
    return this.http.get<NewsItem[]>(environment.apiURL + `/api/news?start=${start}&amount=${amount}`, this.httpGlobal.AuthorizedHTTPOptions);
  }

  getAllPostGroups(): Observable<PostGroup[]> {
    return this.http.get<PostGroup[]>(environment.apiURL + '/api/postgroup', this.httpGlobal.AuthorizedHTTPOptions);
  }
}

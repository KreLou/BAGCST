import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Menu} from '../../models/Menu';
import {DateformatService} from '../dateformat.service';

@Injectable({
  providedIn: 'root'
})
export class MenuloaderService {

  constructor(private http: HttpClient, private dateformat: DateformatService) {
  }
  getMenu(start: Date, end: Date, placeID: number): Observable<Menu[]> {
    return this.http.get<Menu[]>(`http://172.17.17.76:5000/api/Menu?startDate=${this.dateformat.toDateString(start)}
    &endDate=${this.dateformat.toDateString(end)}&placeIDs=${placeID}`);
  }
}

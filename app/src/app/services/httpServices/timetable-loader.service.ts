import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {TimetableItem} from "../../models/TimetableItem";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TimetableLoaderService {

  constructor(private http: HttpClient) { }

  public getTimetable(): Observable<TimetableItem[]> {
    return this.http.get<TimetableItem[]>(environment.apiURL + '/api/timetable');
  }
}

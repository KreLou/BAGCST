import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {TimetableItem} from "../../models/TimetableItem";

@Injectable({
  providedIn: 'root'
})
export class TimetableLoaderService {

  constructor(private http: HttpClient) { }

  public getTimetable(): Observable<TimetableItem[]> {
    return this.http.get<TimetableItem[]>('http://172.17.17.141:5000/api/Timetable');
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {TimetableItem} from "../../models/TimetableItem";
import { environment } from 'src/environments/environment';
import { GlobalHTTPService } from './global-http.service';

@Injectable({
  providedIn: 'root'
})
export class TimetableLoaderService {

  constructor(private http: HttpClient, private httpConfig: GlobalHTTPService) { }

  public getTimetable(): Observable<TimetableItem[]> {
    return this.http.get<TimetableItem[]>(environment.apiURL + '/api/timetable', this.httpConfig.AuthorizedHTTPOptions);
  }

  public downloadTimetableFile(): Observable<Blob> {
    return this.http.get<any>(environment.apiURL +  '/api/timetable/export', this.httpConfig.AuthorizedHTTPOptions);
  }
}

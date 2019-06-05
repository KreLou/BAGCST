import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, BehaviorSubject, Subscriber} from "rxjs";
import {TimetableItem} from "../../models/TimetableItem";
import { environment } from 'src/environments/environment';
import { GlobalHTTPService } from './global-http.service';
import { CacheService } from '../cache.service';

@Injectable({
  providedIn: 'root'
})
export class TimetableLoaderService {

  private timetableList: BehaviorSubject<TimetableItem[]> = new BehaviorSubject([]);

  constructor(private http: HttpClient, private httpConfig: GlobalHTTPService, private cache: CacheService) {
    this.initializeTimetable();
   }


  private initializeTimetable() {
    this.loadOfflineTimetable();
    this.http.get<TimetableItem[]>(environment.apiURL + '/api/timetable', this.httpConfig.AuthorizedHTTPOptions).subscribe(data => {
      console.log('Timetable', data);
      this.cache.setValue('timetable', data);
      this.formatAndSave(data as TimetableItem[]);
    }, error => {
      console.error(error);
    }, () => {
      console.log('Request done');
    })
  }

  private loadOfflineTimetable() {
    const cache = this.cache.getValue('timetable');
    if (cache && this.timetableList.value.length == 0) {
      this.formatAndSave(cache);
    }
  }

  /**
   * Format the date and save the in the BehaviorSubject
   * @author KreLou
   * @param items 
   */
  private formatAndSave(items: TimetableItem[]) {
    items.forEach(x => {
      x.start = new Date(x.start),
      x.end = new Date(x.end)
    });
    this.timetableList.next(items);
  }

  public getTimetable(): Observable<TimetableItem[]> {
    return this.timetableList.asObservable();
  }

  public getSelectedTimetableItems(start: Date, end: Date): Observable<TimetableItem[]> {
    return Observable.create((obserable: Subscriber<any>) => {
      
      var items = this.timetableList.value;
      items = items.filter(x => x.start >= start && x.end <= end);
      obserable.next(items);
      obserable.complete();
    })
  }

  public downloadTimetableFile(): Observable<Blob> {
    return this.http.get<any>(environment.apiURL +  '/api/timetable/export', this.httpConfig.AuthorizedHTTPOptions);
  }
}

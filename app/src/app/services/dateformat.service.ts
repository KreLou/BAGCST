import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateformatService {

  constructor() { }
  toDateString (datum: Date): string {
    const year = datum.getFullYear().toString();
    const month = (datum.getMonth() + 1).toString();
    const day = datum.getDate().toString();

    const formattedDate = year + '-' +  month + '-' + day;
    return formattedDate;
  }
}

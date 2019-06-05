import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateGeneratorService {
  
  MillisecondsPerDay = 1000 * 60 * 60 *24;

  constructor() { }

  /**
   * Gets Today with time 00:00:00:00
   * @author KreLou
   * @returns date of beginning of today 
   */
  getBeginningOfToday(): Date {
    var today = new Date();
    today.setHours(0,0,0,0);
    return today;
  }

  /**
   * Gets Today with time 23:59:59:5999
   * @author KreLou
   * @returns date of end of today 
   */
  getEndOfToday(): Date {
    return new Date(this.getBeginningOfToday().getTime() + this.MillisecondsPerDay);
  }

  /**
   * Add number of days to given date
   * @author KreLou
   * @param date 
   * @param days 
   * @returns date of days to date 
   */
  addDaysToDate(date: Date, days: number): Date {
    return new Date(date.getTime() + this.MillisecondsPerDay * days);
  }
}

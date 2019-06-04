import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateGeneratorService {

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
    const MillisecondsPerDay = 1000 * 60 * 60 *24;
    return new Date(this.getBeginningOfToday().getTime() + MillisecondsPerDay);
  }
}

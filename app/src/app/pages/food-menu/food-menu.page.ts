import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.page.html',
  styleUrls: ['./food-menu.page.css'],
})
export class FoodMenuPage implements OnInit {
  currentDate;
  formattedDate;
  weekNumber;
  constructor() {
    this.currentDate = new Date()
    this.getFormattedDate()
    this.weekNumber = this.getWeekNumber(new Date());
  }
  getFormattedDate(){
    var dateObj = new Date()

    var year = dateObj.getFullYear().toString()
    var month = dateObj.getMonth().toString()
    var date = dateObj.getDate().toString()

    var monthArray = ['Januar','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember']

    this.formattedDate = date + '. ' + monthArray[month] + ' ' + year
  }
  getWeekNumber(d) {
    // Copy date so don't modify original
    d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));
    // Set to nearest Thursday: current date + 4 - current day number
    // Make Sunday's day number 7
    d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay()||7));
    // Get first day of year
    var yearStart : any = new Date(Date.UTC(d.getUTCFullYear(),0,1));
    // Calculate full weeks to nearest Thursday
    var weekNo = Math.ceil(( ( (d - yearStart) / 86400000) + 1)/7);
    // Return array of year and week number
    return [weekNo];
  }

  startAndEndOfWeek(d) {
    var weekMap = [6, 0, 1, 2, 3, 4, 5];
    var now = new Date(d);
    now.setHours(0, 0, 0, 0);
    var monday = new Date(now);
    monday.setDate(monday.getDate() - weekMap[monday.getDay()]);
    var sunday = new Date(now);
    sunday.setDate(sunday.getDate() - weekMap[sunday.getDay()] + 6);
    sunday.setHours(23, 59, 59, 999);
    return [monday, sunday];
  }

  ngOnInit() {}
}

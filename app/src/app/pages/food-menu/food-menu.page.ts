import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.page.html',
  styleUrls: ['./food-menu.page.css'],
})
export class FoodMenuPage implements OnInit {
  currentDate;
  formattedDate
  constructor() {
    this.currentDate = new Date()
    this.getFormattedDate()
  }
  getFormattedDate(){
    var dateObj = new Date()

    var year = dateObj.getFullYear().toString()
    var month = dateObj.getMonth().toString()
    var date = dateObj.getDate().toString()

    var monthArray = ['Januar','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember']

    this.formattedDate = date + '. ' + monthArray[month] + ' ' + year
  }
  ngOnInit() {}
}

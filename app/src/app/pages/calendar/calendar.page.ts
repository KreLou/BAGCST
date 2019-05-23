import { Component, OnInit, ViewChild } from '@angular/core';
import { TimetableItem } from "../../models/TimetableItem";
import {TimetableLoaderService} from 'src/app/services/httpServices/timetable-loader.service';
import {expand} from 'rxjs/operators';
import { IonInfiniteScroll } from '@ionic/angular';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.page.html',
  styleUrls: ['./calendar.page.css'],
})
export class CalendarPage implements OnInit {

    noMoreLectureItemsToShow = false;

    today: Date;
    enddate: Date;
    listUpcomingLectures: any;
    listTimetable: TimetableItem[];
    displayTimetable: any;


    CONST_HowManyItemsAddedAtEndOfView = 3;
    CONST_HowManyMilliSecondsPerDay = 1000 * 60 * 60 * 24;

    constructor(private timetableloader: TimetableLoaderService){
        this.today = new Date();
        this.today.setHours(0,0,0,0);
        this.enddate = new Date(this.today.getTime() + (this.CONST_HowManyMilliSecondsPerDay * 8));
    }

  ngOnInit(): void {
        this.timetableloader.getTimetable().subscribe(data => {

            this.listTimetable = data;
            this.displayTimetable = new Array();
            var days = Array.from(new Set(this.listTimetable.map(x => x.start.toString().split('T')[0])));

            days.forEach(date => {
                this.displayTimetable.push({
                    date: new Date(date),
                    lectures: this.listTimetable.filter(x => x.start.toString().indexOf(date) > -1),
                    expand: false,
                });
            });
            this.listUpcomingLectures = this.displayTimetable.filter(x => x.date >= this.today);
            this.displayTimetable = this.listUpcomingLectures.filter(x => x.date <= this.enddate);
            this.displayTimetable[0].expand = true;
        });
  }
  changeExpandable(day: any) {
        console.log(day);
        day.expand = !day.expand;
  }

  /**
   * Loads more data into displayTimeTable
   * @author KreLou
   * @param event 
   */
  loadData(event: any) {
    /**use timeout, to display the laoding spinner for show time */  
    setTimeout(() => {
        const dates = this.displayTimetable.map(x => x.date);
        const lastday = dates[dates.length - 1];
        const nextElements = this.listUpcomingLectures.filter(x => x.date > lastday);
        for (var i = 0; i < this.CONST_HowManyItemsAddedAtEndOfView; i++){
            if (nextElements[i]){
                this.displayTimetable.push(nextElements[i]);
            } else {
                this.noMoreLectureItemsToShow = true;
            }
        }
        event.target.complete();
    }, 500);
  }
}




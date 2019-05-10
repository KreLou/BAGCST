import { Component, OnInit } from '@angular/core';
import { TimetableItem } from "../../models/TimetableItem";
import {TimetableLoaderService} from 'src/app/services/httpServices/timetable-loader.service';
import {expand} from 'rxjs/operators';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.page.html',
  styleUrls: ['./calendar.page.css'],
})
export class CalendarPage implements OnInit {
    today: Date;
    enddate: Date;
    listUpcomingLectures: any;
    listTimetable: TimetableItem[];
    displayTimetable: any;
    constructor(private timetableloader: TimetableLoaderService){}
  ngOnInit(): void {
        this.today = new Date();
        this.today.setHours(0,0,0,0);
        this.enddate = new Date(this.today.getTime() + (1000 * 60 * 60 * 24 * 8));
        this.timetableloader.getTimetable().subscribe(data => {
            console.table(data);
            this.listTimetable = data;
            this.displayTimetable = new Array();
            var days = Array.from(new Set(this.listTimetable.map(x => x.start.toString().split('T')[0])));
            console.log('days', days);
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
            console.log('this.displayTimeTable', this.displayTimetable);
        });
  }
  changeExpandable(day: any) {
        console.log(day);
        day.expand = !day.expand;
  }
  loadData(event: any) {
        const dates = this.displayTimetable.map(x => x.date);
        const lastday = dates[dates.length - 1];
        const nextElements = this.listUpcomingLectures.filter(x => x.date > lastday);
        for (var i = 0; i < 3; i++){
            if (nextElements[i]){
                this.displayTimetable.push(nextElements[i]);
            }
        }
        console.log(lastday);
  }
}




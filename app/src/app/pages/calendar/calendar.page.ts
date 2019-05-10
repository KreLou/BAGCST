import { Component, OnInit } from '@angular/core';
import { TimetableItem } from "../../models/TimetableItem";
import {TimetableLoaderService} from 'src/app/services/httpServices/timetable-loader.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.page.html',
  styleUrls: ['./calendar.page.css'],
})
export class CalendarPage implements OnInit {
    today: Date;
    listTimetable: TimetableItem[];
    displayTimetable: any;
    constructor(private timetableloader: TimetableLoaderService){}
  ngOnInit(): void {
        this.today = new Date();
        this.timetableloader.getTimetable().subscribe(data => {
            console.table(data);
            this.listTimetable = data;
            this.displayTimetable = new Array();
            var days = Array.from(new Set(this.listTimetable.map(x => x.start)));
            days.forEach(x =>{
                return x.toDateString();
            })
            console.log(days);
        })
  }
}



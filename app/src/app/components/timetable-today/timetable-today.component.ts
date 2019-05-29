import { Component, OnInit } from '@angular/core';
import { TimetableItem } from 'src/app/models/TimetableItem';
import { TimetableLoaderService } from 'src/app/services/httpServices/timetable-loader.service';

@Component({
  selector: 'app-timetable-today',
  templateUrl: './timetable-today.component.html',
  styleUrls: ['./timetable-today.component.css']
})
export class TimetableTodayComponent implements OnInit {

  lectureItems: TimetableItem[];

  today: Date;
  endOfDay: Date;

  constructor(private timetableLoader: TimetableLoaderService) {
    this.today = new Date();
    this.today.setHours(0,0,0,0);

    this.endOfDay = new Date();
    this.endOfDay.setHours(23, 59, 59, 59);
   }

  ngOnInit() {
    this.timetableLoader.getTimetable().subscribe(data => {
      data = data as TimetableItem[];
      data.forEach(x => {
        x.start = new Date(x.start);
        x.end = new Date(x.end);
      })
      this.lectureItems = data.filter(x => x.start >= this.today).filter(x => x.end <= this.endOfDay);

    })
  }

}

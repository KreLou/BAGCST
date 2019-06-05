import { Component, OnInit } from '@angular/core';
import { TimetableItem } from 'src/app/models/TimetableItem';
import { TimetableLoaderService } from 'src/app/services/httpServices/timetable-loader.service';
import { DateGeneratorService } from 'src/app/services/date-generator.service';

@Component({
  selector: 'app-timetable-today',
  templateUrl: './timetable-today.component.html',
  styleUrls: ['./timetable-today.component.css']
})
export class TimetableTodayComponent implements OnInit {

  lectureItems: TimetableItem[];

  beginOfDay: Date;
  endOfDay: Date;

  constructor(private timetableLoader: TimetableLoaderService, private dateGenerator: DateGeneratorService) {
    this.beginOfDay = this.dateGenerator.getBeginningOfToday();
    this.endOfDay = this.dateGenerator.getEndOfToday();
   }

  ngOnInit() {
    this.timetableLoader.getSelectedTimetableItems(this.beginOfDay, this.endOfDay).subscribe(data => {
      console.log('FIlter data: ', data);
      this.lectureItems = data;
    })
  }

}

import { Component, OnInit, ViewChild } from '@angular/core';
import { TimetableItem } from "../../models/TimetableItem";
import {TimetableLoaderService} from 'src/app/services/httpServices/timetable-loader.service';
import {expand, filter} from 'rxjs/operators';
import { IonInfiniteScroll, AlertController } from '@ionic/angular';
import { DateGeneratorService } from 'src/app/services/date-generator.service';

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

    constructor(private timetableloader: TimetableLoaderService, private alertController: AlertController, private dateGenerator: DateGeneratorService){
        this.today = this.dateGenerator.getBeginningOfToday();
        this.enddate = this.dateGenerator.addDaysToDate(this.today, 8);
        console.log('Enddate', this.enddate);
    }

  ngOnInit(): void {
        this.timetableloader.getTimetable().subscribe(data => {


            this.listTimetable = Object.assign([], data);

            this.displayTimetable = new Array();

            const daysWithTime = data.map(x => x.start);

            var days: any[] = [];
            
            daysWithTime.forEach(day => {
                const date = new Date(day);
                date.setHours(0,0,0,0);
                const found =days.filter(x => x.getTime() === date.getTime()).length > 0;
                if (!found) {
                    days.push(date);
                }
            });

            console.log('days', days);


            
            days.forEach(date => {
                const filteredEvents = this.listTimetable.filter(x => x.start.getDate() === date.getDate() && x.start.getMonth() === date.getMonth() && x.start.getFullYear() === date.getFullYear());
                console.log('day', date);
                console.log('Found', filteredEvents);
                this.displayTimetable.push({
                    date: date,
                    lectures: filteredEvents,
                    expand: false
                });
            })
            console.log('DisplayTimetable', this.displayTimetable);

            this.listUpcomingLectures = this.displayTimetable.filter(x => x.date >= this.today);

            console.log('Upcomming', this.listUpcomingLectures);
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

  onDownloadClick() {
      console.log('Download click');
      this.timetableloader.downloadTimetableFile().subscribe(data => {
          console.log('data');
      });
  }
}




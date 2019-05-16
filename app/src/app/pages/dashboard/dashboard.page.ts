import { Component, OnInit } from '@angular/core';
import {MenuController} from '@ionic/angular';
import {Router} from '@angular/router';
import { TimetableItem } from "../../models/TimetableItem";
import {TimetableLoaderService} from 'src/app/services/httpServices/timetable-loader.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.css'],
})
export class DashboardPage implements OnInit {

	sideMenuPages = [
		{title: 'Administration', url: 'administrator', icon: undefined},
		{title: 'Einstellungen', url: 'settings', icon: undefined},
		{title: 'Impressum', url: 'imprint', icon: undefined},
		{title: 'Datenschutz', url: 'privacy', icon: undefined},
		{title: 'Über diese APP', url: 'about', icon: undefined},
	]

  timeTableItem: TimetableItem;
  today: Date;
    enddate: Date;
    listUpcomingLectures: any;
    listTimetable: TimetableItem[];
    displayTimetable: any;

    CONST_HowManyItemsAddedAtEndOfView = 3;
    CONST_HowManyMilliSecondsPerDay = 1000 * 60 * 60 * 24;

  constructor(private menu: MenuController, private router: Router, private timeTableLoader: TimetableLoaderService) {
    this.today = new Date();
    this.today.setHours(0,0,0,0);
    this.enddate = new Date(this.today.getTime() + (this.CONST_HowManyMilliSecondsPerDay * 8));
   }

   ngOnInit(): void {
    this.menu.enable(true, 'dashboardMenu');
    this.timeTableLoader.getTimetable().subscribe(data => {

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
    })};

  toggleMenu(): void {
    this.menu.toggle('dashboardMenu');
  }

  navigateTo(url: string) {
    console.log('Navigate to ', url);
  }


}

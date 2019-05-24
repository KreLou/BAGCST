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
  displayTimetable: any;

  constructor(private menu: MenuController) { }

   ngOnInit() {}

  toggleMenu(): void {
    this.menu.toggle('dashboardMenu');
  }

  navigateTo(url: string) {
    console.log('Navigate to ', url);
  }


}

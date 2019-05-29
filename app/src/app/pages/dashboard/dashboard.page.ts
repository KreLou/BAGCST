import { Component, OnInit } from '@angular/core';
import {MenuController, ModalController} from '@ionic/angular';
import {Router} from '@angular/router';


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


  constructor(private menu: MenuController, private router: Router,
    private modalController: ModalController) { }

   ngOnInit() {}

  toggleMenu(): void {
    this.menu.toggle('dashboardMenu');
  }
  navigateTo(url: string) {
    console.log('Navigate to ', url);
  }


}

import { Component, OnInit } from '@angular/core';
import {MenuController} from '@ionic/angular';
import {Router} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.css'],
})
export class DashboardPage implements OnInit {

	sideMenuPages = [
		{title: 'Administration', url: 'administrator', icon: undefined},
		{title: 'Settings', url: 'settings', icon: undefined},
		{title: 'Imprint', url: 'imprint', icon: undefined},
		{title: 'Privacy', url: 'privacy', icon: undefined},
		{title: 'About', url: 'about', icon: undefined},
	]

  constructor(private menu: MenuController, private router: Router) { }

  ngOnInit() {
    this.menu.enable(true, 'dashboardMenu');
  }

  toggleMenu(): void {
    this.menu.toggle('dashboardMenu');
  }


}

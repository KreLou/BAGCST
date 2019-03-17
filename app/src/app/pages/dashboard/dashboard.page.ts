import { Component, OnInit } from '@angular/core';
import {MenuController} from '@ionic/angular';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.css'],
})
export class DashboardPage implements OnInit {

  constructor(private menu: MenuController) { }

  ngOnInit() {
    this.menu.enable(true, 'dashboardMenu');
  }

  toggleMenu(): void {
    this.menu.toggle('dashboardMenu');
  }


}

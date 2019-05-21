import { Component, OnInit } from '@angular/core';
import {MenuController, ModalController} from '@ionic/angular';
import {Router} from '@angular/router';
import { LoginComponent } from 'src/app/components/login/login.component';

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

  constructor(private menu: MenuController, private router: Router,
    private modalController: ModalController) { }


  ngOnInit() {
    this.menu.enable(true, 'dashboardMenu');
  }

  toggleMenu(): void {
    this.menu.toggle('dashboardMenu');
  }

  async showModal(): Promise<any> {
    try{
      const loginModal = await this.modalController.create({
        component: LoginComponent
      });
      console.log(loginModal);
      return await loginModal.present();
    }catch(ex) {
      console.log(ex);
    }
  navigateTo(url: string) {
    console.log('Navigate to ', url);
  }


}

import { Component } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { TabsService } from './services/tabs.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent {
  sideMenuPages = [
		{title: 'Einstellungen', url: 'settings', icon: 'settings'},
		{title: 'Impressum', url: 'imprint', icon: 'list'},
		{title: 'Datenschutz', url: 'privacy', icon: 'finger-print'},
		{title: 'Über diese APP', url: 'about', icon: 'pricetag'},
	]
  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private tabs: TabsService,
    private router: Router
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  navigateTo(url: string){
    console.log('Navigate to ', url);
    this.router.navigate([url]);
  }
}

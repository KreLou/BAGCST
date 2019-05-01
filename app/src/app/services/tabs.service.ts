import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Platform } from '@ionic/angular';
import { filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TabsService {

  private hideTabBarPages: string[] = [
    'login',
  ];

  constructor(private router: Router, private platform: Platform) { 
    this.platform.ready().then(() => {
      this.navEvents();
    });
  }

  private navEvents() {
    this.router.events.pipe(filter(e => e instanceof NavigationEnd))
    .subscribe((e: any) => {
      this.showHideTabs(e.url);
    });
  }

  public hideTabs() {
    const tabBar = document.getElementById('navigationBar');
    if (tabBar.style.display !== 'none') tabBar.style.display = 'none';
  }

  public showTabs() {
    const tabBar = document.getElementById('navigationBar');
    if (tabBar.style.display !== 'flex') tabBar.style.display = 'flex';
  }

  private showHideTabs(url: string) {
    url = url.split('/')[1];

    const shouldHide = this.hideTabBarPages.indexOf(url) > -1;

    try{
      setTimeout(() => {
        shouldHide ? this.hideTabs() : this.showTabs();
      }, 300);
    }catch(err) {

    }
  }
}

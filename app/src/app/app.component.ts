import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  navigationButtons = [
    {list: '', displayText: 'Dashboard', icon: 'home'},
    {link: 'contacts', displayText: 'Ansprechpartner', icon: 'people'},
    {link: 'calendar', displayText: 'Kalender', icon: 'calendar_today'},
    {link: 'menu', displayText: 'Speisekarte', icon: 'view_list'},
    {link: 'news', displayText: 'Newsfeed', icon: 'rss_feed'}
  ];

}

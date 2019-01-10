import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  activeLink = null;

  navigationButtons = [
    {link: 'calendar', displayText: 'Kalender', icon: 'calendar_today'},
    {link: 'menu', displayText: 'Speisekarte', icon: 'fastfood'},
    {link: 'news', displayText: 'Newsfeed', icon: 'rss_feed'},
    {link: 'contacts', displayText: 'Ansprechpartner', icon: 'people'}
  ];

  /*
   * Set the active Link to change the font-Color
   */
  setActiveNavigation(link: string): void {
    this.activeLink = link;
  }

}

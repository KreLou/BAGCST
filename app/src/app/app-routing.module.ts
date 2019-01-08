import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ImprintComponent} from './Global/imprint/imprint.component';
import {ContactsOverviewComponent} from './contacts/contacts-overview/contacts-overview.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {MenuOverviewComponent} from './menu/menu-overview/menu-overview.component';
import {CalendarOverviewComponent} from './calendar/calendar-overview/calendar-overview.component';
import {NewsFeedOverviewComponent} from './news-feed/news-feed-overview/news-feed-overview.component';

const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'imprint', component: ImprintComponent},
  {path: 'contacts', component: ContactsOverviewComponent},
  {path: 'menu', component: MenuOverviewComponent},
  {path: 'calendar', component: CalendarOverviewComponent},
  {path: 'news', component: NewsFeedOverviewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

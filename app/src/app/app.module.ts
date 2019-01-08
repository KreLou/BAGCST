import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButton, MatButtonModule, MatIconModule, MatSidenavModule, MatToolbarModule} from '@angular/material';
import { ImprintComponent } from './Global/imprint/imprint.component';
import {ContactsModule} from './contacts/contacts.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import {CalendarModule} from './calendar/calendar.module';
import {MenuModule} from './menu/menu.module';
import {NewsFeedModule} from './news-feed/news-feed.module';

@NgModule({
  declarations: [
    AppComponent,
    ImprintComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    ContactsModule,
    CalendarModule,
    MenuModule,
    NewsFeedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

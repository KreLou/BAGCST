import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarOverviewComponent } from '../calendar/calendar-overview/calendar-overview.component';

@NgModule({
  declarations: [CalendarOverviewComponent],
  imports: [
    CommonModule
  ],
  exports: [CalendarOverviewComponent]
})
export class CalendarModule { }

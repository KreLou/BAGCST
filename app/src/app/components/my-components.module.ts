import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealTodayComponent } from './meal-today/meal-today.component';
import { TimetableTodayComponent } from './timetable-today/timetable-today.component';

@NgModule({
  declarations: [
    MealTodayComponent,
    TimetableTodayComponent
],
  imports: [
    CommonModule
  ],
  exports: [
    MealTodayComponent,
    TimetableTodayComponent
  ]
})
export class MyComponentsModule { }

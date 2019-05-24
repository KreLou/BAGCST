import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealTodayComponent } from './meal-today/meal-today.component';
import { TimetableTodayComponent } from './timetable-today/timetable-today.component';
import { IonicModule } from '@ionic/angular';

@NgModule({
  declarations: [
    MealTodayComponent,
    TimetableTodayComponent
],
  imports: [
    CommonModule,
    IonicModule
  ],
  exports: [
    MealTodayComponent,
    TimetableTodayComponent
  ]
})
export class MyComponentsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { AdminFoodPlanerPage } from './admin-food-planer.page';

const routes: Routes = [
  {
    path: '',
    component: AdminFoodPlanerPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AdminFoodPlanerPage]
})
export class AdminFoodPlanerPageModule {}

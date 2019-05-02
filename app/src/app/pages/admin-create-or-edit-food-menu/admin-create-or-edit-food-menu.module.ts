import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { AdminCreateOrEditFoodMenuPage } from './admin-create-or-edit-food-menu.page';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: AdminCreateOrEditFoodMenuPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AdminCreateOrEditFoodMenuPage]
})
export class AdminCreateOrEditFoodMenuPageModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { DashboardPage } from './dashboard.page';
import { NewsFeedModule } from 'src/app/components/NewsFeed/news-feed.module';
import { MyComponentsModule } from 'src/app/components/my-components.module';


const routes: Routes = [
  {
    path: '',
    component: DashboardPage
  }
];

@NgModule({
  imports: [
    NewsFeedModule,
    CommonModule,
    FormsModule,
    IonicModule,
    MyComponentsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [DashboardPage],
})
export class DashboardPageModule {}

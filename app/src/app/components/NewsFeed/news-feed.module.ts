import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsFeedComponent } from './news-feed/news-feed.component';
import { IonicModule } from '@ionic/angular';

@NgModule({
  declarations: [NewsFeedComponent],
  imports: [
    CommonModule,
    IonicModule
  ],
  exports:[
    NewsFeedComponent
  ]
})
export class NewsFeedModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsFeedOverviewComponent } from './news-feed-overview/news-feed-overview.component';

@NgModule({
  declarations: [NewsFeedOverviewComponent],
  imports: [
    CommonModule
  ],
  exports: [NewsFeedOverviewComponent]
})
export class NewsFeedModule { }

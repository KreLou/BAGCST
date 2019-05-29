import { Component, OnInit, ViewChild } from '@angular/core';
import { NewsItem } from 'src/app/models/NewsItem';
import { NewsFeedLoaderService } from 'src/app/services/httpServices/news-feed-loader.service';
import { IonInfiniteScroll } from '@ionic/angular';

@Component({
  selector: 'app-news-feed',
  templateUrl: './news-feed.component.html',
  styleUrls: ['./news-feed.component.css']
})
export class NewsFeedComponent implements OnInit {

  endOfNewsFeed = false;

  newsList: NewsItem[];

  constructor(private newsFeedLoader: NewsFeedLoaderService) { }

  ngOnInit() {
    const maxValue = 2147483000;
    this.loadFeed(maxValue,10);
  }

  loadFeed(start: number, amount: number) {
    console.log('Load id: ', start);
    
    this.newsFeedLoader.load(start, amount).subscribe(data => {

      if (this.newsList){
        const newsItems: NewsItem[] = data;
        newsItems.forEach((item) => {
          this.newsList.push(item);
        });
      }else {
        this.newsList = data;
      }
      console.log(this.newsList);
    });
  }

  loadData(event) {
    const minID = this.getMinUsedID();
    //Stop loading, if minID is 1
    if (minID > 1){
      this.loadFeed(minID -1 , 10);
    } else {
      event.target.disabled = true;
    }
    event.target.complete();
  }



  /**
   * Gets min used id from newsList
   * @author KreLou
   * @returns  
   */
  getMinUsedID() {
    return Math.min.apply(Math, this.newsList.map(x => x.id));
  }

}

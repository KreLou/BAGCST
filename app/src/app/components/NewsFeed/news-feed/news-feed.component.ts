import { Component, OnInit } from '@angular/core';
import { NewsItem } from 'src/app/models/NewsItem';
import { NewsFeedLoaderService } from 'src/app/services/httpServices/news-feed-loader.service';

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
    this.loadFeed(0,10);
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

  loadData(event: CustomEvent) {
    console.log('Load infos', event);
    const minID = this.getMinUsedID();
    this.loadFeed(minID -1 , 10);
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

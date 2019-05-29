import { Component, OnInit, ViewChild } from '@angular/core';
import { NewsItem } from 'src/app/models/NewsItem';
import { NewsFeedLoaderService } from 'src/app/services/httpServices/news-feed-loader.service';
import { IonInfiniteScroll, AlertController } from '@ionic/angular';
import { PostGroup } from 'src/app/models/PostGroup';
import { MELoaderService } from 'src/app/services/httpServices/meloader.service';
import { Router } from '@angular/router';
import { ActivatedRouteGuard } from 'src/app/guards/activated-route.guard';

@Component({
  selector: 'app-news-feed',
  templateUrl: './news-feed.component.html',
  styleUrls: ['./news-feed.component.css']
})
export class NewsFeedComponent implements OnInit {

  endOfNewsFeed = false;

  newsList: NewsItem[];

  postGroups: PostGroup[];

  subscribedGroups: PostGroupSubscribtionPushSetting[];

  constructor(private newsFeedLoader: NewsFeedLoaderService, 
    private alert: AlertController, 
    private meLoader: MELoaderService, 
    private router: Router,
    public routeGuard: ActivatedRouteGuard) { }

  maxValue = 2147483000;

  ngOnInit() {

    this.loadSubscribedPostGroups();

    this.newsFeedLoader.getAllPostGroups().subscribe(data => {
      this.postGroups = data;
    });
    this.loadFeed(this.maxValue,10);
  }

  /**
   * Loads the next News-Items
   * @author KreLou
   * @param start 
   * @param amount 
   */
  loadFeed(start: number, amount: number) {
    
    this.newsFeedLoader.load(start, amount).subscribe(data => {

      if (this.newsList){
        const newsItems: NewsItem[] = data;
        newsItems.forEach((item) => {
          if (this.newsList.filter(x => x.id === item.id).length === 0){ //Only if not allready implements
            this.newsList.push(item);
          }
        });
      }else {
        this.newsList = data;
      }
      console.log(this.newsList);
    });
  }

  private loadSubscribedPostGroups() {
    this.meLoader.getSubscribedPostGroups().subscribe(data => {
      this.subscribedGroups = data;
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
   * Opens selection
   * @author KreLou
   */
  openSelection(){
    var inputs = [];
    this.postGroups.forEach(group => {
      inputs.push({
        name: group.name,
        type: 'checkbox',
        label: group.name,
        value: group.postGroupID,
        checked: this.subscribedGroups.filter(x => x.postGroupID === group.postGroupID).length === 1
      });
    });
    this.alert.create({
      header: 'Gruppen',
      inputs: inputs,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        }, {
          text: 'Ok',
          handler: (event) => {
            this.handleUserPostGroupSelection(event);
          }
        }
      ]
    }).then((obj) => {
      obj.present();
    })
  }

  /**
   * Handles user post group selection
   * @author KreLou
   * @param input 
   */
  handleUserPostGroupSelection(input: number[]) {
    var sendSubscribedGroups: PostGroupSubscribtionPushSetting[] = [];
    input.forEach(id => {
      sendSubscribedGroups.push({
        postGroupID: id,
        PostGroupActive: true
      })
    });

    this.meLoader.setSubscribedPostGroups(sendSubscribedGroups).subscribe(data => {
      this.loadSubscribedPostGroups();


      this.loadFeed(this.maxValue, 10);
    })
  } 

  createNewPost(){
    console.log('New Post');
    this.router.navigate(['tabs', 'admin-new-post'])
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

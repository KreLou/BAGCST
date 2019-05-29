import { Component, OnInit } from '@angular/core';
import { PostGroup } from 'src/app/models/PostGroup';
import { MELoaderService } from 'src/app/services/httpServices/meloader.service';
import { NewsItem } from 'src/app/models/NewsItem';
import { NewsFeedLoaderService } from 'src/app/services/httpServices/news-feed-loader.service';
import { PopUpMessageService } from 'src/app/services/pop-up-message.service';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-new-post',
  templateUrl: './admin-new-post.page.html',
  styleUrls: ['./admin-new-post.page.css'],
})
export class AdminNewPostPage implements OnInit {

  postGroups: PostGroup[];

  newsItem = {
    authorID: 0,
    date: new Date(),
    id: 0,
    message: '',
    title: '',
    postGroup: undefined,
    postGroupID: 0
  }

  constructor(private meLoader: MELoaderService,
    private newsLoader: NewsFeedLoaderService,
    private router: Router,
    private popup: PopUpMessageService) {}

  ngOnInit() {
    this.meLoader.getPostGroupsWhereIAmTheAuthor().subscribe(data => {
      this.postGroups = data;
      console.log('PostGroups: ', this.postGroups);
    })
  }

  save() {
    console.log('Save');
    this.newsItem.postGroup = this.postGroups.filter(x => x.postGroupID === this.newsItem.postGroupID)[0];
    console.log(this.newsItem);

    if (this.newsItem.message.length === 0){
      this.popup.showInputError('Bitte eine Nachricht eintragen');
      return;
    } else if (this.newsItem.title.length === 0) {
      this.popup.showInputError('Bitte einen Titel eintragen');
      return;
    } else if(this.newsItem.postGroupID === 0) {
      this.popup.showInputError('Bitte eine Gruppe auswählen');
      return;
    }

    this.newsLoader.postNewNewsItem(this.newsItem).subscribe(data => {
      console.log('War erfolgreich');
      this.popup.showAPISuccess('Erfolgreich gepostet');
      this.router.navigate(['']);
    })

  }

}

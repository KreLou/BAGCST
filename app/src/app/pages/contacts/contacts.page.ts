import { Component, OnInit } from '@angular/core';
import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import {Injectable} from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.page.html',
  styleUrls: ['./contacts.page.css'],
})

@NgModule({
  declarations:[],
  imports: [BrowserModule, HttpClientModule]
})

export class ContactsPage implements OnInit {

  constructor() {
    // let Testitem:IonItem = document.element getElementById('testID')
    // var TestVariable:string = 'TestText';
    // Testitem = TestVariable;
    
  }
  

  ngOnInit() {
  }



}
@Injectable()
export class Configuration{
  public server = 'http://localhost:50179/';
  public apiUrl = 'api/';
  public serverWithApiUrl = this.server + this.apiUrl;
}

export var contactList:string[] = ["TestItem1", "TestItem2", "TestItem3"];

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.page.html',
  styleUrls: ['./contacts.page.css'],
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

export class AppComponent{
  title = 'Tour of Heroes';
  myHero = 'Windstorm';
}

// export var contactList:string[] = ["TestItem1", "TestItem2", "TestItem3"];

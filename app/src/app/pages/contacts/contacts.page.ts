import { Component, OnInit } from '@angular/core';
import {Injectable} from '@angular/core';
import { ContactsLoaderService } from 'src/app/services/httpServices/contacts-loader.service';
import { ContactItem } from 'src/app/models/ContactItem';
import {ContactDetailsPage} from 'src/app/pages/contact-details/contact-details.page';
import { NavController } from '@ionic/angular';
import {Router} from '@angular/router';


@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.page.html',
  styleUrls: ['./contacts.page.css'],
})

export class ContactsPage implements OnInit {

  contactList: ContactItem[];
  contactDetailsPage: ContactDetailsPage;

  constructor(
    private contactLoader: ContactsLoaderService,
    public navCtrl: NavController,
    public router: Router
  ) {}

  ngOnInit() {
    this.contactLoader.getAllContacts().subscribe(data => {
      console.log(data);
      this.contactList = data;
    }, error => {console.error(error); });
  }

  goToDetails(id:number){
    // ContactDetailsPage.contactID = id;
    console.log(id);
    this.router.navigate(['contacts',id.toString()]);
  }

}

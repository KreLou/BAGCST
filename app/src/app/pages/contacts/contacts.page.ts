import { Component, OnInit } from '@angular/core';
import {Injectable} from '@angular/core';
import { ContactsLoaderService } from 'src/app/services/httpServices/contacts-loader.service';
import { ContactItem } from 'src/app/models/ContactItem';


@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.page.html',
  styleUrls: ['./contacts.page.css'],
})

export class ContactsPage implements OnInit {

  contactList: ContactItem[];

  constructor(
    private contactLoader: ContactsLoaderService
  ) {}

  ngOnInit() {
    this.contactLoader.getAllContacts().subscribe(data => {console.log(data);}, error => {console.error(error); });
    this.contactLoader.getAllContacts().subscribe(data => {this.contactList = data;});    
  }

}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ContactsLoaderService } from 'src/app/services/httpServices/contacts-loader.service';
import { ContactItem } from 'src/app/models/ContactItem';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.page.html',
  styleUrls: ['./contact-details.page.css'],
})

export class ContactDetailsPage implements OnInit {


  /**
   * Contact id of contact details page
   */
  public contactID: number;
  contactItem: ContactItem;


  constructor(private activedRoute: ActivatedRoute, private contactLoader: ContactsLoaderService) {
    this.activedRoute.params.subscribe(paraMap => {
      this.contactID = paraMap['contactID'];
      console.log('ContactID: ', this.contactID);
    });
   }

  ngOnInit() {
    this.contactItem = this.contactLoader.getContact(this.contactID);
  }

}

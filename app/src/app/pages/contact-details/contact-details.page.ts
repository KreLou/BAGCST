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
  contactID: number;
  contactItem: ContactItem


  constructor(private activedRoute: ActivatedRoute, private contactLoader:ContactsLoaderService) {
    this.activedRoute.params.subscribe(paraMap => {
      this.contactID = paraMap['contactID'];
      console.log('ContactID: ', this.contactID);

      //Hier kannst du jetzt weitermachen, die variable contactID beinhaltet die ID der aufgerufenen Seite

    })
   }

  ngOnInit() {
    this.contactLoader.getContact(this.contactID).subscribe(data => {console.log(data);}, error => {console.error(error);});
    this.contactLoader.getContact(this.contactID).subscribe(data => {this.contactItem = data;});
  }

}

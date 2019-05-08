import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

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


  constructor(private activedRoute: ActivatedRoute) {
    this.activedRoute.params.subscribe(paraMap => {
      this.contactID = paraMap['contactID'];
      console.log('ContactID: ', this.contactID);

      //Hier kannst du jetzt weitermachen, die variable contactID beinhaltet die ID der aufgerufenen Seite
    })
   }

  ngOnInit() {
  }

}

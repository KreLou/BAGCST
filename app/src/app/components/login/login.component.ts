import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private modalController: ModalController) { }

  ngOnInit() {
  }

  dismis() {
    this.modalController.dismiss();
  }
}

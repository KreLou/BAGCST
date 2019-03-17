import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.css'],
})
export class LoginPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  doLogin(): void {
    console.log('Login');
  }

}

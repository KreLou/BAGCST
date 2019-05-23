import { Component, OnInit } from '@angular/core';
import { IonInput } from '@ionic/angular';
import { UserAuthLoaderService } from 'src/app/services/httpServices/user-auth-loader.service';
import { TokenService } from 'src/app/services/token.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.css'],
})
export class LoginPage implements OnInit {

  constructor(private registerService: UserAuthLoaderService, private router: Router, private tokenLoader: TokenService) { }

  ngOnInit() {
  }

  doLogin(input: IonInput): void {
    const email = input.value;
    input.value = '';
    console.log('Login ' + email);

    this.registerService.sendUserRegistration(email, 'test').subscribe(data => {
      console.log('Data', data);
      const token = data.token;

      console.log('Token', token);
      this.tokenLoader.saveToken(token);

      this.router.navigate(['']);
    }, error => {
      console.error(error);
    })
  }

}

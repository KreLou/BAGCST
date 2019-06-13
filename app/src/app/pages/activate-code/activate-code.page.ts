import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAuthLoaderService } from 'src/app/services/httpServices/user-auth-loader.service';
import { reject } from 'q';
import { PopUpMessageService } from 'src/app/services/pop-up-message.service';

export enum activationStatus {
  Waiting = 'Waiting',
  Accepted = 'Accpted',
  Rejected = 'Rejected',
  NotFound = 'NotFound'
}

@Component({
  selector: 'app-activate-code',
  templateUrl: './activate-code.page.html',
  styleUrls: ['./activate-code.page.css'],
})
export class ActivateCodePage implements OnInit {

  status: activationStatus = activationStatus.Waiting;
  code;

  constructor(private activedRoute: ActivatedRoute, private userLoader: UserAuthLoaderService, private router: Router, private popup: PopUpMessageService) { }

  ngOnInit() {
    this.activedRoute.paramMap.subscribe(data => {
      this.code = data['params']['code'];

      this.userLoader.sendActivation(this.code).subscribe(data => {
        console.log('data', data);
        this.status = activationStatus.Accepted;
        this.popup.showAPISuccess('Sitzung aktiviert');
        this.router.navigate(['']);
      }, error => {
        console.log('error', error);
        if (error.status === 404) {
          this.status = activationStatus.NotFound;
        }else {
          this.status = activationStatus.Rejected;
        }
      });
    })
  }

}

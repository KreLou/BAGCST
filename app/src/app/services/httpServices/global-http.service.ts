import { Injectable } from '@angular/core';
import {TokenService} from './../token.service';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GlobalHTTPService {

  constructor(private tokenService: TokenService) { }

  public AnonymousHTTPOptions = {
    headers: new HttpHeaders ({
    'Content-Type': 'application/json'
    })
  };

  public AuthorizedHTTPOptions = {
    headers: new HttpHeaders ({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + this.tokenService.getToken()
    })
  };
}

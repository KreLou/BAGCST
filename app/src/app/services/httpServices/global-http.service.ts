import { Injectable } from '@angular/core';
import {TokenService} from './../token.service';

@Injectable({
  providedIn: 'root'
})
export class GlobalHTTPService {

  constructor(private tokenService: TokenService) { }

  public AnonymousHTTPOptions = {
    'Content-Type': 'application/json'
  };

  public AuthorizedHTTPOptions = {
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + this.tokenService.getToken()
  };
}

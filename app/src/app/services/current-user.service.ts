import { Injectable } from '@angular/core';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  constructor(private tokenService: TokenService) {

   }

  public IsAuthenticated(): boolean {
    if (this.tokenService.getToken() ===  null) {
      return false;
    }
    return true;
  }
}

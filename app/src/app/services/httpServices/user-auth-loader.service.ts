import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { GlobalHTTPService } from './global-http.service';

@Injectable({
  providedIn: 'root'
})
export class UserAuthLoaderService {

  constructor(private http: HttpClient, private globalHttp: GlobalHTTPService) { }

  public sendUserRegistration(email: string, device: string): Observable<any> {
    const postObjects = {
      'email': email,
      'devicename': device
    };
    return this.http.post(environment.apiURL + '/api/auth/register', postObjects, this.globalHttp.AnonymousHTTPOptions);
  }


  
}

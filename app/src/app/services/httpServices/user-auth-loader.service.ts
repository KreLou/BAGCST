import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class UserAuthLoaderService {

  constructor(private http: HttpClient) { }

  public sendUserRegistration(email: string, device: string): Observable<any> {
    const postObjects = {
      'username': email,
      'devicename': device
    };
    return this.http.post(environment.apiURL + '/api/auth/register', postObjects);
  }
}

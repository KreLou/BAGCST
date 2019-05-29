import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalHTTPService } from './global-http.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class MELoaderService {

  constructor(private http: HttpClient, private httpConfig: GlobalHTTPService) { }


  public getUserPermissionGroups(): Observable<any> {
    return this.http.get(environment.apiURL + '/api/me/rights', this.httpConfig.AuthorizedHTTPOptions);
  }

  public getSubscribedPostGroups(): Observable<PostGroupSubscribtionPushSetting[]> {
    return this.http.get<PostGroupSubscribtionPushSetting[]>(environment.apiURL + '/api/me/postGroups', this.httpConfig.AuthorizedHTTPOptions);
  }

  public setSubscribedPostGroups(settings: PostGroupSubscribtionPushSetting[]): Observable<any> {
    return this.http.post(environment.apiURL + '/api/me/postGroups', settings, this.httpConfig.AuthorizedHTTPOptions);
  }
}

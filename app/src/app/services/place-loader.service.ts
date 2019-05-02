import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlaceLoaderService {

  constructor(private httpClient: HttpClient) { }

  public getPlaces(): Observable<any> {
    return this.httpClient.get(environment.apiURL + '/api/place');
  }
}

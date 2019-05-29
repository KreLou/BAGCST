import { retry, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { Observable, throwError } from 'rxjs';
import { Place } from '../../models/Place';
import { GlobalHTTPService } from './global-http.service';

@Injectable({
  providedIn: 'root'
})
export class PlaceLoaderService {

  constructor(private httpClient: HttpClient, private globalHTTP: GlobalHTTPService) { }

  public getPlaces(): Observable<Place[]> {
    return this.httpClient.get<Place[]>(environment.apiURL + '/api/place', this.globalHTTP.AuthorizedHTTPOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  public getPlaceByID(id: number): Observable<Place> {
    return this.httpClient.get<Place>(environment.apiURL + '/api/place/' + id, this.globalHTTP.AuthorizedHTTPOptions);
  }

  handleError(error) {
     let errorMessage = '';

     if (error.error instanceof ErrorEvent) {
       //Client side error
       errorMessage = `Error: ${error.error.message}`;
     } else {
       //Server side error
       errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
     }
     window.alert(errorMessage);
     return throwError(errorMessage);
  }
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Menu } from '../../models/Menu';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { GlobalHTTPService } from './global-http.service';
import { DateformatService } from '../dateformat.service';

@Injectable({
  providedIn: 'root'
})
export class MenuLoaderService {

  constructor(private http: HttpClient, 
    private globalHTTP: GlobalHTTPService,
    private dateformat: DateformatService) { }

  /**
   * 
   * @param placeid Search for Forcast for this palce
   */
  getMenuForecast(placeid: number): Observable<Menu[]> {
    return this.http.get<Menu[]>(environment.apiURL + `/api/menu?placeids=${placeid}`, this.globalHTTP.AuthorizedHTTPOptions);
  }

  /**
   * Gets menu by id
   * @param id search for ID
   * @returns menu by id 
   */
  getMenuByID(id: number): Observable<Menu> {
    return this.http.get<Menu>(environment.apiURL + `/api/menu/${id}`, this.globalHTTP.AuthorizedHTTPOptions);
  }

  /**
   * Creates new menu item
   * @param menu New complete MenuItem
   * @returns new menu item 
   */
  createNewMenuItem(menu: Menu): Observable<Menu> {
    return this.http.post<Menu>(environment.apiURL + '/api/menu', menu, this.globalHTTP.AuthorizedHTTPOptions);
  }


  /**
   * Updates menu item
   * @param menu Item with new values
   * @returns menu item 
   */
  updateMenuItem(menu: Menu): Observable<Menu> {
    return this.http.put<Menu>(environment.apiURL + '/api/menu/' + menu.menuID, menu, this.globalHTTP.AuthorizedHTTPOptions);
  }

  /**
   * Delete an MenuItem
   * @param menuID MenuID which should deleted
   */
  deleteMenu(menuID: number): Observable<any> {
    return this.http.delete(environment.apiURL + `/api/menu/${menuID}`, this.globalHTTP.AuthorizedHTTPOptions);
  }

  getMenu(start: Date, end: Date, placeID: number): Observable<Menu[]> {
    return this.http.get<Menu[]>(environment.apiURL + `/api/Menu?startDate=${this.dateformat.toDateString(start)}
    &endDate=${this.dateformat.toDateString(end)}&placeIDs=${placeID}`);
  }

}

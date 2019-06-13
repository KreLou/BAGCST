import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, Subscriber } from 'rxjs';
import { Menu } from '../../models/Menu';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { GlobalHTTPService } from './global-http.service';
import { DateformatService } from '../dateformat.service';
import { CacheService } from '../cache.service';

@Injectable({
  providedIn: 'root'
})
export class MenuLoaderService {

  private foodPlan: BehaviorSubject<Menu[]> = new BehaviorSubject([]);

  constructor(private http: HttpClient, 
    private globalHTTP: GlobalHTTPService,
    private dateformat: DateformatService,
    private cache: CacheService) {
      this.laodOfflineFoodPlan();
      this.loadFoodMenuFromAPI();
     }


  /**
   * Loads food menu from api
   * Loads last 7 days and next 14 days
   * @author KreLou
   */
  private loadFoodMenuFromAPI() {
    const startDate = new Date((new Date()).getTime() - (1000 * 60 * 60 *24 * 7));
    const endDate = new Date((new Date()).getTime() + (1000 * 60 * 60 * 24 * 14));

    this.http.get<Menu[]>(environment.apiURL + `/api/menu?startDate=${this.dateformat.toDateString(startDate)}&endDate=${this.dateformat.toDateString(endDate)}`, this.globalHTTP.AuthorizedHTTPOptions).subscribe(data => {
      console.log('Data', data);
      var response = data as Menu[];

      response.forEach(menuItem => {
        menuItem.date = new Date(menuItem.date);
      });
      this.foodPlan.next(response);
      this.cache.setValue('foodmenu', response);
    })

  }

  /**
   * Laods food-menu from storage
   * @author KreLou
   */
  private laodOfflineFoodPlan(){
    const cache = this.cache.getValue('foodmenu');
    if (cache && this.foodPlan.value.length === 0) {
      cache.forEach(element => {
        element.date = new Date(element.date);
      });
      this.foodPlan.next(cache as Menu[]);
    }
  }

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
   * @author KreLou
   * @returns menu by id 
   */
  getMenuByID(id: number): Observable<Menu> {
    return this.http.get<Menu>(environment.apiURL + `/api/menu/${id}`, this.globalHTTP.AuthorizedHTTPOptions);
  }

  /**
   * Creates new menu item
   * @param menu New complete MenuItem
   * @author KreLou
   * @returns new menu item 
   */
  createNewMenuItem(menu: Menu): Observable<Menu> {
    return this.http.post<Menu>(environment.apiURL + '/api/menu', menu, this.globalHTTP.AuthorizedHTTPOptions);
  }


  /**
   * Updates menu item
   * @param menu Item with new values
   * @author KreLou
   * @returns menu item 
   */
  updateMenuItem(menu: Menu): Observable<Menu> {
    return this.http.put<Menu>(environment.apiURL + '/api/menu/' + menu.menuID, menu, this.globalHTTP.AuthorizedHTTPOptions);
  }

  /**
   * Delete an MenuItem
   * @param menuID MenuID which should deleted
   * @author KreLou
   */
  deleteMenu(menuID: number): Observable<any> {
    return this.http.delete(environment.apiURL + `/api/menu/${menuID}`, this.globalHTTP.AuthorizedHTTPOptions);
  }



  /**
   * Gets menu for special place
   * @author KreLou
   * @param start Start Date
   * @param end  End Date
   * @param placeID PlaceID for food-Menu
   * @returns menu for special place 
   */
  getMenuForSpecialPlace(start: Date, end: Date, placeID: number): Observable<Menu[]> {
    return Observable.create((subscriber: Subscriber<any>) => {
      var menu = this.foodPlan.value;
      menu = menu.filter(x => x.date >= start && x.date <= end && x.meal.place.placeID == placeID);
      subscriber.next(menu);
      subscriber.complete();
    })
  }

  

  getMenuForAllPlaces(start: Date, end: Date): Observable<Menu[]> {
    return Observable.create((subscriber: Subscriber<any>) => {
      var menu = this.foodPlan.value;
      menu = menu.filter(x => x.date >= start && x.date <= end);
      subscriber.next(menu);
      subscriber.complete();
    })
  }

}

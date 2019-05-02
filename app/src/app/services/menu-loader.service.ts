import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Menu } from '../models/Menu';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MenuLoaderService {

  constructor(private http: HttpClient) { }

  getMenuForecast(placeid: number): Observable<Menu[]> {
    return this.http.get<Menu[]>(environment.apiURL + `/api/menu?placeids=${placeid}`);
  }

  getMenuByID(id: number): Observable<Menu> {
    return this.http.get<Menu>(environment.apiURL + `/api/menu/${id}`);
  }

  createNewMenuItem(menu: Menu): Observable<Menu> {
    return this.http.post<Menu>(environment.apiURL + '/api/menu', menu);
  }

  updateMenuItem(menu: Menu): Observable<Menu> {
    return this.http.put<Menu>(environment.apiURL + '/api/menu/' + menu.menuID, menu);
  }
}

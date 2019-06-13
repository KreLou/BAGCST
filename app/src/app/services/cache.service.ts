import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  constructor() { }

  public setValue(key: string, value: object) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  public getValue(key: string): any {
    var value = localStorage.getItem(key);

    var object = JSON.parse(value);

    return object;
  }
}

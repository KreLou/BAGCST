import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ContactItem } from 'src/app/models/ContactItem';
import { GlobalHTTPService } from './global-http.service';
import { CacheService } from '../cache.service';

@Injectable({
  providedIn: 'root'
})
export class ContactsLoaderService {

  private contactList: BehaviorSubject<ContactItem[]> = new BehaviorSubject([]);

  constructor(private http: HttpClient, private globalHttp: GlobalHTTPService, private cache: CacheService) {
    this.initializeContacts();
  }

  /**
   * Initializes contacts
   * Checks cache and send reqeust to api
   * @author KreLou
   */
  private initializeContacts() {
    this.loadOfflineContacts();
    this.http.get<ContactItem[]>(environment.apiURL + '/api/contacts', this.globalHttp.AuthorizedHTTPOptions).subscribe(data => {
      console.log('Load Contacts from Server');
      this.contactList.next(data as ContactItem[]);
      this.cache.setValue('contacts', data);
    })
  }
  /**
   * Loads offline contacts from cache
   * @author KreLou
   */
  loadOfflineContacts() {
    const cache = this.cache.getValue('contacts');
    if (cache && this.contactList.value.length == 0) {
      console.log('Restore old contacts');
      this.contactList.next(cache);
    }
  }

  /**
   * Gets all contacts as Observable
   * @author KreLou
   * @returns all contacts 
   */
  getAllContacts(): Observable<ContactItem[]> {
    return this.contactList.asObservable();
  }

  /**
   * Filter specific contactItem by ID
   * @author KreLou
   * @param id Search for
   * @returns contact or undefined
   */
  getContact(id: number): ContactItem | null {
    return this.contactList.getValue().filter(contact => contact.contactID == id)[0];
  }

  /* 
  getAllContacts(): Observable<ContactItem[]> {
    return this.http.get<ContactItem[]>(environment.apiURL + `/api/contacts`, this.globalHttp.AuthorizedHTTPOptions);
  }

  getContact(id: number): Observable<ContactItem> {
    return this.http.get<ContactItem>(environment.apiURL + `/api/contacts/` + id, this.globalHttp.AuthorizedHTTPOptions);
  } */
}

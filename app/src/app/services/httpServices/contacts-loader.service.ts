import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ContactItem } from 'src/app/models/ContactItem';

@Injectable({
  providedIn: 'root'
})
export class ContactsLoaderService {

  constructor(private http: HttpClient) {}

  getAllContacts(): Observable<ContactItem[]> {
    return this.http.get<ContactItem[]>(environment.apiURL + `/api/contacts`);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ContactItem } from 'src/app/models/ContactItem';
import { GlobalHTTPService } from './global-http.service';

@Injectable({
  providedIn: 'root'
})
export class ContactsLoaderService {

  constructor(private http: HttpClient, private globalHttp: GlobalHTTPService) {}

  getAllContacts(): Observable<ContactItem[]> {
    return this.http.get<ContactItem[]>(environment.apiURL + `/api/contacts`, this.globalHttp.AuthorizedHTTPOptions);
  }

  getContact(id: number): Observable<ContactItem> {
    return this.http.get<ContactItem>(environment.apiURL + `/api/contacts/` + id, this.globalHttp.AuthorizedHTTPOptions);
  }
}

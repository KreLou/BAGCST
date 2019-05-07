import { Component, OnInit } from '@angular/core';
import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})

export class ContactsPage implements OnInit {

  constructor(
    private contactLoader: ContactLoaderService
  ) {}
  ngOnInit() {
    this.contactLoader.getAllContacts().subscribe(data => console.log(data));
  }

}

// export let contactList: string[] = ['TestItem1', 'TestItem2', 'TestItem3'];

export interface Contact {
  contactID: number;
  firstName: string;
  lastName: string;
  telNumber: string;
  email: string;
  room: string;
  responsibility: string;
  course: string;
  type: string;
}

export class ContactLoaderService {
  constructor(private http: HttpClient) {}

  getAllContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(environment.apiURL + `/api/contacts`);
  }
}

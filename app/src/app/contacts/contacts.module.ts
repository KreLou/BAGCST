import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsOverviewComponent } from './contacts-overview/contacts-overview.component';

@NgModule({
  declarations: [ContactsOverviewComponent],
  imports: [
    CommonModule
  ],
  exports: [ContactsOverviewComponent]
})
export class ContactsModule { }

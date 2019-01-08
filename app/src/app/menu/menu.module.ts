import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuOverviewComponent } from '../menu/menu-overview/menu-overview.component';

@NgModule({
  declarations: [MenuOverviewComponent],
  imports: [
    CommonModule
  ],
  exports: [MenuOverviewComponent]
})
export class MenuModule { }

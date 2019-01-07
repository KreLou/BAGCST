import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ImprintComponent} from './Global/imprint/imprint.component';

const routes: Routes = [
  {path: 'imprint', component: ImprintComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

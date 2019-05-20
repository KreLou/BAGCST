import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: './tabs/tabs.module#TabsPageModule'},
  { path: 'administrator', loadChildren: './pages/administrator/administrator.module#AdministratorPageModule'},
  { path: 'settings', loadChildren: './../pages/settings/settings.module#SettingsPageModule' },
  { path: 'login', loadChildren: './../pages/login/login.module#LoginPageModule' },   
  { path: 'imprint', loadChildren: './../pages/imprint/imprint.module#ImprintPageModule' },
  { path: 'about', loadChildren: './../pages/about/about.module#AboutPageModule' },
  { path: 'privacy', loadChildren: './../pages/privacy/privacy.module#PrivacyPageModule' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: 'dashboard', loadChildren: './pages/dashboard/dashboard.module#DashboardPageModule'},
    { path: 'contacts', loadChildren: './pages/contacts/contacts.module#ContactsPageModule'},
    { path: 'menu', loadChildren: './pages/food-menu/food-menu.module#FoodMenuPageModule'},
    { path: 'calendar', loadChildren: './pages/calendar/calendar.module#CalendarPageModule'},
  { path: 'settings', loadChildren: './pages/settings/settings.module#SettingsPageModule' },
    
  { path: 'login', loadChildren: './pages/login/login.module#LoginPageModule' },
  { path: 'administrator', loadChildren: './pages/administrator/administrator.module#AdministratorPageModule' },
  { path: 'news-feed', loadChildren: './pages/news-feed/news-feed.module#NewsFeedPageModule' },
  { path: 'imprint', loadChildren: './pages/imprint/imprint.module#ImprintPageModule' },
  { path: 'about', loadChildren: './pages/about/about.module#AboutPageModule' },
  { path: 'privacy', loadChildren: './pages/privacy/privacy.module#PrivacyPageModule' },
  { path: '**', redirectTo: 'dashboard'}

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

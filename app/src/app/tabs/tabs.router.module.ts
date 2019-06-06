import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TabsPage } from './tabs.page';
import { ActivatedRouteGuard } from '../guards/activated-route.guard';

const routes: Routes = [
  {
    path: 'tabs',
    component: TabsPage,
    canActivateChild: [ActivatedRouteGuard],
    children: [  
      {path: 'dashboard', loadChildren: './../pages/dashboard/dashboard.module#DashboardPageModule'},
      { path: 'contacts', loadChildren: './../pages/contacts/contacts.module#ContactsPageModule'},
      { path: 'contacts/:contactID', loadChildren: './../pages/contact-details/contact-details.module#ContactDetailsPageModule' },
      { path: 'menu', loadChildren: './../pages/food-menu/food-menu.module#FoodMenuPageModule'},
      { path: 'calendar', loadChildren: './../pages/calendar/calendar.module#CalendarPageModule'},
      { path: 'news-feed', loadChildren: './../pages/news-feed/news-feed.module#NewsFeedPageModule' },
      { path: 'admin-food-planer', loadChildren: './../pages/admin-food-planer/admin-food-planer.module#AdminFoodPlanerPageModule' },
      { path: 'admin-food-planer/:placeID/:menuID', loadChildren: './../pages/admin-create-or-edit-food-menu/admin-create-or-edit-food-menu.module#AdminCreateOrEditFoodMenuPageModule' },
      { path: 'admin-new-post', loadChildren: './../pages/admin-new-post/admin-new-post.module#AdminNewPostPageModule' },
      { path: '**', redirectTo: 'dashboard'},
        ]
  },
  {
    path: '',
    redirectTo: '/tabs/dashboard',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TabsPageRoutingModule {}

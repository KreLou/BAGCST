import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { ActivatedRouteGuard } from './guards/activated-route.guard';

const routes: Routes = [
  { path: '', loadChildren: './tabs/tabs.module#TabsPageModule', canActivate: [AuthGuard, ActivatedRouteGuard]},
  { path: 'administrator', loadChildren: './pages/administrator/administrator.module#AdministratorPageModule', canActivate: [AuthGuard, ActivatedRouteGuard]},
  { path: 'settings', loadChildren: './pages/settings/settings.module#SettingsPageModule', canActivate: [AuthGuard, ActivatedRouteGuard] },
  { path: 'login', loadChildren: './pages/login/login.module#LoginPageModule' },   
  { path: 'activate/:code', loadChildren: './pages/activate-code/activate-code.module#ActivateCodePageModule' },
  { path: 'imprint', loadChildren: './pages/imprint/imprint.module#ImprintPageModule' },
  { path: 'about', loadChildren: './pages/about/about.module#AboutPageModule' },
  { path: 'privacy', loadChildren: './pages/privacy/privacy.module#PrivacyPageModule' }

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

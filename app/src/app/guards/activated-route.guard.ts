import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MELoaderService } from '../services/httpServices/meloader.service';
import { PopUpMessageService } from '../services/pop-up-message.service';
import { CacheService } from '../services/cache.service';

const CONST_KEY = 'userRoutes'

@Injectable({
  providedIn: 'root'
})
export class ActivatedRouteGuard implements CanActivate, CanActivateChild {

  userRoutes: string[] = ['/tabs/dashboard'];

  constructor(private meLoader: MELoaderService, private router: Router, private popup: PopUpMessageService, private cache: CacheService) {
    console.log('activated-route. constructur');
    const routes = this.cache.getValue(CONST_KEY);

    if (routes) {
      this.userRoutes = routes;
      console.log('Set from cache: ', routes);
    }

    this.meLoader.getUserPermissionGroups().subscribe(data => {
      if (data) {
        this.userRoutes = ['/tabs/dashboard']; //Reset to default value
        data.forEach(group => {
          group.rights.forEach(rightElement => {
            this.userRoutes.push(rightElement.path)
          });
        });
      }
      console.log('userRoutes: ', this.userRoutes);
      this.cache.setValue(CONST_KEY, this.userRoutes);
    });

  }

  /**
   * Determines whether activate can
   * Check if user is allowed to navigate to this page
   * @author KreLou
   * @param next 
   * @param state 
   * @returns activate 
   */
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      const url = state.url;
    if (!this.evalutateRequestedPath(url)){
      this.sendPermissionDeniedMessage();
      return false;
    }
    return true;
  }

  /**
   * Determines whether activate child can
   * Check if user is allowed to navigate to the childs
   * @author KreLou
   * @param childRoute 
   * @param state 
   * @returns true if activate child 
   */
  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this.evalutateRequestedPath(state.url)){
      //this.router.navigate(['']);
      this.sendPermissionDeniedMessage
      ();
      return false;
    }
    return true;
  }
  /**
   * Sends permission denied message
   * @author KreLou
   */
  sendPermissionDeniedMessage() {
    this.popup.showPermissionDeniedForNavigateToRoute('Keine Berechtigung für diesen Bereich');
  }

  /**
   * Evalutates requested path, check if path is in array
   * @author KreLou
   * @param path Requested Path
   * @returns true if requested path 
   * TODO Comment the placeholder *
   */
  evalutateRequestedPath(path: string): boolean {
    if (this.userRoutes.filter(x => path.toLowerCase().indexOf(x.toLowerCase().split('*')[0]) >= 0).length > 0) {
      console.log('Path ', path , 'is allowed');
      return true;
    }
    console.log('Path ', path , 'is rejected');
    return false;
  }
}

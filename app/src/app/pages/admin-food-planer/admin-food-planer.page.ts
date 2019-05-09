import { Component, OnInit } from '@angular/core';
import { PlaceLoaderService } from 'src/app/services/place-loader.service';
import { Place } from 'src/app/models/Place';
import { Menu } from 'src/app/models/Menu';
import { MenuLoaderService } from 'src/app/services/menu-loader.service';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-admin-food-planer',
  templateUrl: './admin-food-planer.page.html',
  styleUrls: ['./admin-food-planer.page.scss'],
})
export class AdminFoodPlanerPage implements OnInit {

  foundedPlaces: Place[];

  activePlace: Place;

  menuForecast: any;

  testID: number;

  constructor(private placeLoader: PlaceLoaderService,
    private menuLoader: MenuLoaderService,
    private router: Router,
    private alertController: AlertController) { }

  ngOnInit() {
  }

  /**
   * Loads data from api and save in variables
   */
  private loadDataFromAPI() {
    this.placeLoader.getPlaces().subscribe(data => {
      this.foundedPlaces = data;
      console.table(this.foundedPlaces);
      if (!this.activePlace) {
        this.foundedPlaces.length > 1 ? this.activePlace = this.foundedPlaces[0] : this.activePlace = undefined;
        this.testID = 0;
      }
      this.handlePlaceSelection();
    });
  }

  ionViewWillEnter(){
    this.loadDataFromAPI();
  }


  /**
   * Handles place selection
   */
  handlePlaceSelection() {
    if (this.activePlace) {
      this.menuLoader.getMenuForecast(this.activePlace.placeID).subscribe(data => {
        console.log(data);

        this.menuForecast = new Array();

        var days = Array.from(new Set(data.map(x => x.date)));

        days.forEach(x => {
          this.menuForecast.push({
            date: x,
            menus: data.filter(dataElement => dataElement.date === x)
          });
        });

        console.log(this.menuForecast);

      })
    }
  }

  /**
   * Determines whether add new menu click on
   */
  onAddNewMenuClick() {
    const placeID = this.activePlace ? this.activePlace.placeID : 0;
    this.router.navigate([ 'tabs', 'admin-food-planer', placeID ,'0']);
  }

  /**
   * Determines whether edit click on
   * @param menu 
   */
  onEditClick(menu: Menu) {
    this.router.navigate(['tabs', 'admin-food-planer', this.activePlace.placeID, menu.menuID]);
  }

  /**
   * Determines whether delete click on
   * @author KreLou
   * @param menu 
   */
  onDeleteClick(menu: Menu) {
    console.log('Delete Menu', menu);
    this.alertController.create({
      header: 'Delete',
      subHeader: menu.meal.mealName,
      message: 'Do you want to delete this item?',
      buttons: [
        {
          text: 'Yes',
          handler: () => {
            this.deleteMenuAndRefreshPage(menu);
          }
        },{
          text: 'No'
        }
      ]
    }).then((obj) => {
      obj.present();
    })
  }

  /**
   * Deletes menu and refresh page
   * @author KreLou
   * @param menu 
   */
  deleteMenuAndRefreshPage(menu: Menu) {
    console.log('Now delete');
    this.menuLoader.deleteMenu(menu.menuID).subscribe(data => {
      this.loadDataFromAPI();
    });
  }

}

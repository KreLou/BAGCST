import { Component, OnInit } from '@angular/core';
import { PlaceLoaderService } from 'src/app/services/place-loader.service';
import { Place } from 'src/app/models/Place';
import { Menu } from 'src/app/models/Menu';
import { MenuLoaderService } from 'src/app/services/menu-loader.service';
import { Router } from '@angular/router';

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
    private router: Router) { }

  ngOnInit() {
    this.placeLoader.getPlaces().subscribe(data => {
      this.foundedPlaces = data;
      console.table(this.foundedPlaces);
      
      this.foundedPlaces.length > 1 ? this.activePlace = this.foundedPlaces[0] : this.activePlace = undefined;
      this.testID = 0;
      this.handlePlaceSelection();
    });
  }
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

  addNewMenu() {
    this.router.navigate(['admin-food-planer', this.activePlace.placeID ,'0']);
  }

  editMenu(menu: Menu) {
    this.router.navigate(['admin-food-planer', this.activePlace.placeID, menu.menuID]);
  }

}

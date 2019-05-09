import { Component, OnInit } from '@angular/core';
import { Menu } from 'src/app/models/Menu';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { MenuController, IonDatetime, IonInput, PickerController } from '@ionic/angular';
import { Meal } from 'src/app/models/Meal';
import { Place } from 'src/app/models/Place';
import { MenuLoaderService } from 'src/app/services/menu-loader.service';
import { PopUpMessageService } from 'src/app/services/pop-up-message.service';
import { PlaceLoaderService } from 'src/app/services/place-loader.service';

@Component({
  selector: 'app-admin-create-or-edit-food-menu',
  templateUrl: './admin-create-or-edit-food-menu.page.html',
  styleUrls: ['./admin-create-or-edit-food-menu.page.scss'],
})
export class AdminCreateOrEditFoodMenuPage implements OnInit {

  menu: Menu;


  inputMenuID: number;
  inputPlaceID: number;


  /**Picker options */
  pickerOptions: any;

  constructor(
    private activatedRoute: ActivatedRoute, 
    private menuLoader: MenuLoaderService, 
    private popup: PopUpMessageService, 
    private placeloader: PlaceLoaderService,
    private router: Router) {
    this.activatedRoute.paramMap.subscribe(params => {
      this.inputPlaceID = params['params']['placeID'];
      this.inputMenuID = params['params']['menuID'];

      console.log(this.inputMenuID);
      this.menu = {} as Menu;
      this.menu.meal = {} as Meal;
      this.menu.meal.place = {} as Place;
      this.menu.meal.place.placeID = this.inputPlaceID;
      this.menu.menuID = this.inputMenuID;
      
      if (this.inputMenuID == 0) { /**Create existing Menu */
        this.menu.date = new Date();
        this.menu.date.setDate(this.menu.date.getDate() + 1); /**TODO Remove date-manipulation */
      }else { /**Edit existing Menu */
        this.menuLoader.getMenuByID(this.inputMenuID).subscribe(data => {
          this.menu = data;
        });
      }

      /** Load Placeinformation */
      this.placeloader.getPlaceByID(this.inputPlaceID).subscribe(data => {
        console.log('Place_Data, ', data);
        this.menu.meal.place = data;
      })
      console.log(this.menu);
    });

    this.pickerOptions = {
      buttons: [{
        text: 'Save',
        handler: () => console.log('Clicked Save!')
      }, {
        text: 'Log',
        handler: () => {
          console.log('Clicked Log. Do not Dismiss.');
          return false;
        }
      }]
    };
   }

  ngOnInit() {

  }

  save() {
    console.log('Menu', this.menu);
    //Handle all errors

    if (this.menu.date < new Date()) {
      console.log('Datum kann nicht in der Vergangenheit liegen');
      this.popup.showInputError('Datum kann nicht in der Vergangenheit liegen');
    } else if (this.menu.meal.mealName.length < 5) {
      console.log('Bitte MealName eintragen');
      this.popup.showInputError('Bitte MealName eintragen. Mind.länge 5 Zeichen');
    } else if(!this.menu.meal.description || this.menu.meal.description.length < 10) {
      console.log('Bitte Beschreibung eintragen');
      this.popup.showInputError('Bitte Beschreibung eingeben. Mind.länge 10 Zeichen');
    } else if (this.menu.price <0 || this.menu.price >=15) {
      console.log('Bitte Preis eintragen');
      this.popup.showInputError('Bitte einen Preis zwischen 0 und 15€ eintragen');
    } else {
      if (this.inputMenuID == 0) {
        this.createNewMenu();
      } else {
        this.updateMenu();
      }
    }
  }
  updateMenu() {
    console.log('updateMenu');
    this.menuLoader.updateMenuItem(this.menu).subscribe(data => {
      console.log('Update successfully', data);
      this.router.navigate(['tabs', 'admin-food-planer']);
      this.popup.showAPISuccess('Eintrag aktualisiert');
    })
  }
  createNewMenu() {
    console.log('CreateMenu');
    this.menuLoader.createNewMenuItem(this.menu).subscribe(data => {
      this.popup.showAPISuccess('Eintrag angelegt');
      this.router.navigate(['tabs', 'admin-food-planer']);
    })
  }

  clearInput(input: IonInput) {
    input.value = '';
  }

  ionDateTimeFocus(input: any) {
    console.log('Focus', input);
  }

}

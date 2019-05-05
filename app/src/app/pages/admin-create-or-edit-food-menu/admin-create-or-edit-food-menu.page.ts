import { Component, OnInit } from '@angular/core';
import { Menu } from 'src/app/models/Menu';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { MenuController, IonDatetime, IonInput } from '@ionic/angular';
import { Meal } from 'src/app/models/Meal';
import { Place } from 'src/app/models/Place';
import { MenuLoaderService } from 'src/app/services/menu-loader.service';
import { PopUpMessageService } from 'src/app/services/pop-up-message.service';

@Component({
  selector: 'app-admin-create-or-edit-food-menu',
  templateUrl: './admin-create-or-edit-food-menu.page.html',
  styleUrls: ['./admin-create-or-edit-food-menu.page.scss'],
})
export class AdminCreateOrEditFoodMenuPage implements OnInit {

  date: string;
  menu: Menu;

  inputMenuID: number;
  inputPlaceID: number;


  /**Picker options */
  pickerOptions: any;

  constructor(private activatedRoute: ActivatedRoute, private menuLoader: MenuLoaderService, private popup: PopUpMessageService) {
    this.activatedRoute.paramMap.subscribe(params => {
      this.inputPlaceID = params['params']['placeID'];
      this.inputMenuID = params['params']['menuID'];

      console.log(this.inputMenuID);
      if (this.inputMenuID == 0) {
        this.menu = {} as Menu;
        this.menu.meal = {} as Meal;
        this.menu.meal.place = {} as Place;
        this.menu.meal.place.placeID = this.inputPlaceID;
        this.menu.menuID = this.inputMenuID;
        this.menu.date = new Date();
        this.date = this.menu.date.toISOString();
        console.log('Datum: ', this.date);
      }else {
        this.menuLoader.getMenuByID(this.inputMenuID).subscribe(data => {
          this.menu = data;
        });
      }
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

  openDateTimePicker(picker: IonDatetime){
    picker.open().then(() => {
    }).catch(() => {
    })
  }

  save() {
    console.log('Menu', this.menu);
    //Handle all errors

    if (this.menu.date < new Date()) {
      this.popup.showInputError('Datum kann nicht in der Vergangenheit liegen');
    } else if (this.menu.meal.mealName.length < 5) {
      this.popup.showInputError('Bitte MealName eintragen. Mind.länge 5 Zeichen');
    } else if(this.menu.meal.description.length < 10) {
      this.popup.showInputError('Bitte Beschreibung eingeben. Mind.länge 10 Zeichen');
    } else if (this.menu.price <0 || this.menu.price >=15) {
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
    this.menuLoader.updateMenuItem(this.menu).subscribe(data => {
      this.popup.showAPISuccess('Eintrag aktuallisiert');
    })
  }
  createNewMenu() {
    this.menuLoader.createNewMenuItem(this.menu).subscribe(data => {
      this.popup.showAPISuccess('Eintrag angelegt');
    })
  }

  clearInput(input: IonInput) {
    input.value = '';
  }

  ionDateTimeFocus(input: any) {
    console.log('Focus', input);
  }

}

import { Component, OnInit } from '@angular/core';
import { MenuLoaderService } from 'src/app/services/httpServices/menu-loader.service';
import { Menu } from 'src/app/models/Menu';

@Component({
  selector: 'app-meal-today',
  templateUrl: './meal-today.component.html',
  styleUrls: ['./meal-today.component.css']
})
export class MealTodayComponent implements OnInit {

  menuPlan: any[];

  today: Date;

  constructor(private menuLoader: MenuLoaderService) {
    this.today = new Date();
    this.today.setHours(0, 0, 0, 0);
    console.log('Today', this.today);
   }

  ngOnInit() {
    this.menuLoader.getMenuForAllPlaces(this.today, this.today).subscribe(data => {
      console.log('data', data);

      const places = Array.from(new Set(data.map(x => x.meal.place.placeID)));
      this.menuPlan = new Array();
      places.forEach(x => {
        this.menuPlan.push({
          placeID: x,
          meals: data.filter(meal => meal.meal.place.placeID === x)
        })
      });

      console.log('Menu', this.menuPlan);
    })
  }

}

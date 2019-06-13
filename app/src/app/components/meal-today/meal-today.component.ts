import { Component, OnInit } from '@angular/core';
import { MenuLoaderService } from 'src/app/services/httpServices/menu-loader.service';
import { Menu } from 'src/app/models/Menu';
import { DateGeneratorService } from 'src/app/services/date-generator.service';

@Component({
  selector: 'app-meal-today',
  templateUrl: './meal-today.component.html',
  styleUrls: ['./meal-today.component.css']
})
export class MealTodayComponent implements OnInit {

  menuPlan: any[];

  today: Date;

  constructor(private menuLoader: MenuLoaderService, private dateGenerator: DateGeneratorService) {
    this.today = this.dateGenerator.getBeginningOfToday();
   }

  ngOnInit() {
    this.menuLoader.getMenuForAllPlaces(this.today, this.today).subscribe(data => {

      const places = Array.from(new Set(data.map(x => x.meal.place.placeID)));
      this.menuPlan = new Array();
      places.forEach(x => {
        this.menuPlan.push({
          placeID: x,
          meals: data.filter(meal => meal.meal.place.placeID === x)
        })
      });

    })
  }

}

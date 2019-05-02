import { Component, OnInit } from '@angular/core';
import { PlaceLoaderService } from 'src/app/services/place-loader.service';
import { Place } from 'src/app/models/Place';

@Component({
  selector: 'app-admin-food-planer',
  templateUrl: './admin-food-planer.page.html',
  styleUrls: ['./admin-food-planer.page.css'],
})
export class AdminFoodPlanerPage implements OnInit {

  foundedPlaces: Place[];

  activePlace: Place;

  constructor(private placeLoader: PlaceLoaderService) { }

  ngOnInit() {
    this.placeLoader.getPlaces().subscribe(data => {
      this.foundedPlaces = data;
      console.table(this.foundedPlaces);
      
      this.foundedPlaces.length > 1 ? this.activePlace = this.foundedPlaces[0] : this.activePlace = undefined;
    });
  }

}

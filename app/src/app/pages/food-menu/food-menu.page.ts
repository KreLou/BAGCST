import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.page.html',
  styleUrls: ['./food-menu.page.css'],
})
export class FoodMenuPage implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  goToAdminPage() {
    this.router.navigate(['admin-food-planer']);
  }

}

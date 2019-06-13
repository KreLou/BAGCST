import { Component, OnInit } from '@angular/core';
import {Menu} from '../../models/Menu';
import { Router } from '@angular/router';
import { MenuLoaderService } from 'src/app/services/httpServices/menu-loader.service';
import { ActivatedRouteGuard } from 'src/app/guards/activated-route.guard';

@Component({
    selector: 'app-food-menu',
    templateUrl: './food-menu.page.html',
    styleUrls: ['./food-menu.page.css'],
})
export class FoodMenuPage implements OnInit {
    currentDate;
    weekNumber;
    startEndDate;
    foodMenu: Menu[];
    firstDayofWeek: Date;
    lastDayofWeek: Date;
    constructor(
        private menuloader: MenuLoaderService, private router: Router,
        public activationGuard: ActivatedRouteGuard
    ) {
        this.currentDate = new Date();
        
        this.weekNumber = this.getWeekNumber(new Date());
        this.startEndDate = this.startAndEndOfWeek(new Date());
    }

    getWeekNumber(d) {
        // Copy date so don't modify original
        d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));
        // Set to nearest Thursday: current date + 4 - current day number
        // Make Sunday's day number 7
        d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay() || 7));
        // Get first day of year
        const yearStart: any = new Date(Date.UTC(d.getUTCFullYear(), 0, 1));
        // Calculate full weeks to nearest Thursday
        const weekNo = Math.ceil(( ( (d - yearStart) / 86400000) + 1) / 7);
        // Return array of year and week number
        return [weekNo];
    }

    startAndEndOfWeek(d) {
        // Array of Weekdays
        const weekMap = [6, 0, 1, 2, 3, 4, 5];
        // get actual date
        const now = new Date(d);
        now.setHours(0, 0, 0, 0);
        // set Monday
        const monday = new Date(now);
        // set Sunday
        monday.setDate(monday.getDate() - weekMap[monday.getDay()]);
        const sunday = new Date(now);
        sunday.setDate(sunday.getDate() - weekMap[sunday.getDay()] + 6);
        sunday.setHours(23, 59, 59, 999);
        this.firstDayofWeek = monday;
        this.lastDayofWeek = sunday;
        // formatting Date
        const monMonth = monday.getMonth().toString();
        const monDate = monday.getDate().toString();
        const sunMonth = sunday.getMonth().toString();
        const sunDate = sunday.getDate().toString();

        const monthArray = ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni', 'Juli', 'August', 'September',
            'Oktober', 'November', 'Dezember'];

        const startAndEndofWeek = monDate + '.' + ' ' + monthArray[monMonth] + ' ' + '-' + ' ' + sunDate + '.' + ' ' + monthArray[sunMonth];
        return [startAndEndofWeek];
    }
    placeSwap (id: number) {
        this.showFoodplan(id);
    }
    showFoodplan (id: number) {
        this.menuloader.getMenuForSpecialPlace(this.firstDayofWeek, this.lastDayofWeek, id).subscribe(data => {
            this.foodMenu = data;
        }, () => {}, () => {
        })
    }
    ngOnInit() {
        this.showFoodplan(1);
    }
  goToAdminPage() {
    this.router.navigate(['tabs', 'admin-food-planer']);
  }

}

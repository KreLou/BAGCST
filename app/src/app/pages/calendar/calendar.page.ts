import { Component, OnInit } from '@angular/core';
import { TimetableItem } from "../../models/TimetableItem";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.page.html',
  styleUrls: ['./calendar.page.css'],
})
export class CalendarPage implements OnInit {
    slideOpts = {
        initialSlide: 0,
        speed: 400
    };
  ngOnInit(): void {
  }
}



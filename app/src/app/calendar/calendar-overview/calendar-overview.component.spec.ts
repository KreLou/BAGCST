import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarOverviewComponent } from './calendar-overview.component';

describe('CalendarOverviewComponent', () => {
  let component: CalendarOverviewComponent;
  let fixture: ComponentFixture<CalendarOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

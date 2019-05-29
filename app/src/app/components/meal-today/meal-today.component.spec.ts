import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MealTodayComponent } from './meal-today.component';

describe('MealTodayComponent', () => {
  let component: MealTodayComponent;
  let fixture: ComponentFixture<MealTodayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MealTodayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MealTodayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

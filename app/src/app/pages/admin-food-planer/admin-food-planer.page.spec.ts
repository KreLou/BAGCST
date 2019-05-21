import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFoodPlanerPage } from './admin-food-planer.page';

describe('AdminFoodPlanerPage', () => {
  let component: AdminFoodPlanerPage;
  let fixture: ComponentFixture<AdminFoodPlanerPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminFoodPlanerPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminFoodPlanerPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodMenuPage } from './food-menu.page';

describe('FoodMenuPage', () => {
  let component: FoodMenuPage;
  let fixture: ComponentFixture<FoodMenuPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FoodMenuPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FoodMenuPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

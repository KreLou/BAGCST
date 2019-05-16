import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCreateOrEditFoodMenuPage } from './admin-create-or-edit-food-menu.page';

describe('AdminCreateOrEditFoodMenuPage', () => {
  let component: AdminCreateOrEditFoodMenuPage;
  let fixture: ComponentFixture<AdminCreateOrEditFoodMenuPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCreateOrEditFoodMenuPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCreateOrEditFoodMenuPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

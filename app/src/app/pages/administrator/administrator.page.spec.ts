import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorPage } from './administrator.page';

describe('AdministratorPage', () => {
  let component: AdministratorPage;
  let fixture: ComponentFixture<AdministratorPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministratorPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

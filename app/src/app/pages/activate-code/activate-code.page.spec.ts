import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivateCodePage } from './activate-code.page';

describe('ActivateCodePage', () => {
  let component: ActivateCodePage;
  let fixture: ComponentFixture<ActivateCodePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivateCodePage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivateCodePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

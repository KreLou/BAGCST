import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminNewPostPage } from './admin-new-post.page';

describe('AdminNewPostPage', () => {
  let component: AdminNewPostPage;
  let fixture: ComponentFixture<AdminNewPostPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminNewPostPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminNewPostPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

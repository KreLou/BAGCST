import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsFeedPage } from './news-feed.page';

describe('NewsFeedPage', () => {
  let component: NewsFeedPage;
  let fixture: ComponentFixture<NewsFeedPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewsFeedPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsFeedPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

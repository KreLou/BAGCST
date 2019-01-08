import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsFeedOverviewComponent } from './news-feed-overview.component';

describe('NewsFeedOverviewComponent', () => {
  let component: NewsFeedOverviewComponent;
  let fixture: ComponentFixture<NewsFeedOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewsFeedOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsFeedOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

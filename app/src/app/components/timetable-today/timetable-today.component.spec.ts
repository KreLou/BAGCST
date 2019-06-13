import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimetableTodayComponent } from './timetable-today.component';

describe('TimetableTodayComponent', () => {
  let component: TimetableTodayComponent;
  let fixture: ComponentFixture<TimetableTodayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimetableTodayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimetableTodayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

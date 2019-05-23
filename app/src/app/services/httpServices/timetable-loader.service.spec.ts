import { TestBed } from '@angular/core/testing';

import { TimetableLoaderService } from './timetable-loader.service';

describe('TimetableLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimetableLoaderService = TestBed.get(TimetableLoaderService);
    expect(service).toBeTruthy();
  });
});

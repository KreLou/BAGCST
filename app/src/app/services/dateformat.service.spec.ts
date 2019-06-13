import { TestBed } from '@angular/core/testing';

import { DateformatService } from './dateformat.service';

describe('DateformatService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DateformatService = TestBed.get(DateformatService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { DateGeneratorService } from './date-generator.service';

describe('DateGeneratorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DateGeneratorService = TestBed.get(DateGeneratorService);
    expect(service).toBeTruthy();
  });
});

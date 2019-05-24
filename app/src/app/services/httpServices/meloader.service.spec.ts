import { TestBed } from '@angular/core/testing';

import { MELoaderService } from './meloader.service';

describe('MELoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MELoaderService = TestBed.get(MELoaderService);
    expect(service).toBeTruthy();
  });
});

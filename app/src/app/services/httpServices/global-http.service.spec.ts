import { TestBed } from '@angular/core/testing';

import { GlobalHTTPService } from './global-http.service';

describe('GlobalHTTPService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GlobalHTTPService = TestBed.get(GlobalHTTPService);
    expect(service).toBeTruthy();
  });
});

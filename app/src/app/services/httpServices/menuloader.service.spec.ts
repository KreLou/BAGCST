import { TestBed } from '@angular/core/testing';

import { MenuloaderService } from './menuloader.service';

describe('MenuloaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MenuloaderService = TestBed.get(MenuloaderService);
    expect(service).toBeTruthy();
  });
});

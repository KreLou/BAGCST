import { TestBed } from '@angular/core/testing';

import { MenuLoaderService } from './menu-loader.service';

describe('MenuLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MenuLoaderService = TestBed.get(MenuLoaderService);
    expect(service).toBeTruthy();
  });
});

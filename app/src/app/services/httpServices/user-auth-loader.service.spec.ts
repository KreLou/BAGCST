import { TestBed } from '@angular/core/testing';

import { UserAuthLoaderService } from './user-auth-loader.service';

describe('UserAuthLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserAuthLoaderService = TestBed.get(UserAuthLoaderService);
    expect(service).toBeTruthy();
  });
});

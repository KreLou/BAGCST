import { TestBed } from '@angular/core/testing';

import { UserSettingsLoaderService } from './user-settings-loader.service';

describe('UserSettingsLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserSettingsLoaderService = TestBed.get(UserSettingsLoaderService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { DeviceInformationLoaderService } from './device-information-loader.service';

describe('DeviceInformationLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeviceInformationLoaderService = TestBed.get(DeviceInformationLoaderService);
    expect(service).toBeTruthy();
  });
});

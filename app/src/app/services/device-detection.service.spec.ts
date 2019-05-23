import { TestBed } from '@angular/core/testing';

import { DeviceDetectionService } from './device-detection.service';

describe('DeviceDetectionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeviceDetectionService = TestBed.get(DeviceDetectionService);
    expect(service).toBeTruthy();
  });
});

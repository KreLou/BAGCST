import { TestBed } from '@angular/core/testing';

import { PlaceLoaderService } from './place-loader.service';

describe('PlacesLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PlaceLoaderService = TestBed.get(PlaceLoaderService);
    expect(service).toBeTruthy();
  });
});

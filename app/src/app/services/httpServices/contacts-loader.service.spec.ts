import { TestBed } from '@angular/core/testing';

import { ContactsLoaderService } from './contacts-loader.service';

describe('ContactsLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ContactsLoaderService = TestBed.get(ContactsLoaderService);
    expect(service).toBeTruthy();
  });
});

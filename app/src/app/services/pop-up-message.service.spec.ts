import { TestBed } from '@angular/core/testing';

import { PopUpMessageService } from './pop-up-message.service';

describe('PopUpMessageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PopUpMessageService = TestBed.get(PopUpMessageService);
    expect(service).toBeTruthy();
  });
});

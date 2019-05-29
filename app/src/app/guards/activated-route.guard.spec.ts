import { TestBed, async, inject } from '@angular/core/testing';

import { ActivatedRouteGuard } from './activated-route.guard';

describe('ActivatedRouteGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ActivatedRouteGuard]
    });
  });

  it('should ...', inject([ActivatedRouteGuard], (guard: ActivatedRouteGuard) => {
    expect(guard).toBeTruthy();
  }));
});

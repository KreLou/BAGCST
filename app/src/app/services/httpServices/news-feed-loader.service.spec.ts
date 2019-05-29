import { TestBed } from '@angular/core/testing';

import { NewsFeedLoaderService } from './news-feed-loader.service';

describe('NewsFeedLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NewsFeedLoaderService = TestBed.get(NewsFeedLoaderService);
    expect(service).toBeTruthy();
  });
});

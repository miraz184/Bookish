import { TestBed } from '@angular/core/testing';

import { GoogleBookApiService } from './google-book-api.service';

describe('GoogleBookApiService', () => {
  let service: GoogleBookApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GoogleBookApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

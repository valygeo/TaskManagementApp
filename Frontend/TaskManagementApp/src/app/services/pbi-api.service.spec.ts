import { TestBed } from '@angular/core/testing';

import { PbiApiService } from './pbi-api.service';

describe('PbiApiService', () => {
  let service: PbiApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PbiApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

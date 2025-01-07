import { TestBed } from '@angular/core/testing';

import { PbiService } from './pbi.service';

describe('PbiService', () => {
  let service: PbiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PbiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

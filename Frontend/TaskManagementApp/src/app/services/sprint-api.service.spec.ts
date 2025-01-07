import { TestBed } from '@angular/core/testing';

import { SprintApiService } from './sprint-api.service';

describe('SprintApiService', () => {
  let service: SprintApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SprintApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

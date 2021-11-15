import { TestBed } from '@angular/core/testing';

import { PassHashService } from './pass-hash.service';

describe('PassHashService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PassHashService = TestBed.get(PassHashService);
    expect(service).toBeTruthy();
  });
});

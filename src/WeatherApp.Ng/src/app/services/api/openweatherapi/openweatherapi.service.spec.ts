import { TestBed } from '@angular/core/testing';

import { OpenweatherapiService } from './openweatherapi.service';

describe('OpenweatherapiService', () => {
  let service: OpenweatherapiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpenweatherapiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

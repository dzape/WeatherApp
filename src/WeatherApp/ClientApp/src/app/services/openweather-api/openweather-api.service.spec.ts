import { TestBed } from '@angular/core/testing';

import { OpenWeatherApiService } from './openweather-api.service';

describe('OpenWeatherApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OpenWeatherApiService = TestBed.get(OpenWeatherApiService);
    expect(service).toBeTruthy();
  });
});

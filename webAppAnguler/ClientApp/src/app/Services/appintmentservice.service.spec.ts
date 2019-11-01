import { TestBed } from '@angular/core/testing';

import { AppintmentService } from './appintmentservice.service';

describe('AppintmentserviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AppintmentService = TestBed.get(AppintmentService);
    expect(service).toBeTruthy();
  });
});

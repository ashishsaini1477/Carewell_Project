import { TestBed, inject } from '@angular/core/testing';

import { JobcardService } from './jobcard.service';

describe('Jobcard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JobcardService]
    });
  });

  it('should be created', inject([JobcardService], (service: JobcardService) => {
    expect(service).toBeTruthy();
  }));
});

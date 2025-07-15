import { TestBed } from '@angular/core/testing';

import { QuestionServices } from './question-services';

describe('QuestionServices', () => {
  let service: QuestionServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

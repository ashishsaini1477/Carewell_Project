import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobcardFormComponent } from './jobcard-form.component';

describe('JobcardFormComponent', () => {
  let component: JobcardFormComponent;
  let fixture: ComponentFixture<JobcardFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobcardFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobcardFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

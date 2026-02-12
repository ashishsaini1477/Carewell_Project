import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobcardListComponent } from './jobcard-list.component';

describe('JobcardListComponent', () => {
  let component: JobcardListComponent;
  let fixture: ComponentFixture<JobcardListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobcardListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobcardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

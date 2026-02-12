import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobcardAddComponent } from './jobcard-add.component';

describe('JobcardAddComponent', () => {
  let component: JobcardAddComponent;
  let fixture: ComponentFixture<JobcardAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobcardAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobcardAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

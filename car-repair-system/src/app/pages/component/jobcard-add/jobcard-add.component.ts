import { Component, OnInit } from '@angular/core';
import { JobCardReq } from '../../models/jobcard';
import { Extra } from '../../models/extraModel';
import { Router } from '@angular/router';
import { JobcardService } from '../../services/jobcard.service';
import { ValidationService } from '../../../shared/services/validation.service';

@Component({
  selector: 'app-jobcard-add',
  templateUrl: './jobcard-add.component.html',
  styleUrls: ['./jobcard-add.component.css']
})
export class JobcardAddComponent implements OnInit {

  public jobcard: JobCardReq = new JobCardReq();
  public extra: Extra;
  

  constructor(
    private jobcardService: JobcardService,
    public validation: ValidationService,
    private router: Router,
    
  ) {
    this.jobcard = new JobCardReq();
    this.extra = new Extra();
  }

  ngOnInit() {
  }

  submitJobcard() {
    console.log("submitJobcard",this.jobcard);
    this.jobcardService.addJobcardWithVehicle(this.jobcard).subscribe(
      res => {
        this.alertStatus('Item Added', 200);
        console.log(res);
        this.router.navigate(['/jobcard-list']);
      },
      err => {
        this.alertStatus('something wrong', 404);
        console.log(err);
      }
    );
  }
 private alertStatus(status, code) {
    this.extra.code = code;
    this.extra.status = status;
  }
  
}

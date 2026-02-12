import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Extra } from '../../models/extraModel';
import { ValidationService } from '../../../shared/services/validation.service';
import { Router } from '@angular/router';
import { JobcardService } from '../../services/jobcard.service';
import { JobCard, JobCardStatus, Vehicle } from '../../models/jobcard';
import { Pagination } from '../../models/pagination';
import { PagedResponse } from '../../models/customer';

@Component({
  selector: 'app-jobcard-list',
  templateUrl: './jobcard-list.component.html',
  styleUrls: ['./jobcard-list.component.css']
})
export class JobcardListComponent implements OnInit {


  private modalReference: NgbModalRef;
  public extra: Extra;
  public jobcard: JobCard;
  public pagi: Pagination;
  prop: string = '';
  reverse: boolean = false;
  public jobcardArray: JobCard[] = [];
  constructor(
    private jobcardService: JobcardService,
    private modalService: NgbModal,
    public validation: ValidationService,
    private router: Router
  ) {
    this.extra = new Extra();
    this.pagi = new Pagination();
    this.jobcard = new JobCard();
    this.jobcard.vehicle = new Vehicle();
    this.jobcard.jobCardStatus = new JobCardStatus();
  }

  ngOnInit() {
    this.getAllJobcard();
  }

  getAllJobcard() {
    this.jobcardService.getalljobcords(this.pagi.page, this.pagi.itemPerPage).subscribe(
      (res: PagedResponse<JobCard>) => {
        debugger
        this.jobcardArray = res.data;

         this.pagi.totalItem = res.totalRecords;
          this.pagi.totalPages = Math.ceil(
            this.pagi.totalItem / this.pagi.itemPerPage
          );
      },
      err => console.log(err)
    );
  }

 trackByJobCard(index: number, jobcard: JobCard) {
    return jobcard.jobCardId;
  }

  initAdd(content) {
    this.alertStatus('', 0);
    this.extra.edit = false;
    this.jobcard = new JobCard();
    // open modal
    this.modalReference = this.modalService.open(content);
  }
  private alertStatus(status, code) {
    this.extra.code = code;
    this.extra.status = status;
  }
  // sorting
sortTable(property: string)  : void{
    if (this.prop === property) {
      this.reverse = !this.reverse;
    } else {
      this.prop = property;
      this.reverse = false;
    }
    this.jobcardArray.sort((a: any, b: any) => {
      const x = a[this.prop];
      const y = b[this.prop];
      if (x == null) return 1;
      if (y == null) return -1;
      if (x > y) return this.reverse ? -1 : 1;
      if (x < y) return this.reverse ? 1 : -1;
      return 0;
    });
  }
  nextPage() {
    debugger
    if (this.pagi.page < this.pagi.totalPages) {
      this.pagi.page++;
      this.getAllJobcard();
    }
  }

  prevPage() {
    if (this.pagi.page > 1) {
      this.pagi.page--;
      this.getAllJobcard();
    }
  }
  get pageSizeOptions(): number[] {
    const options = [5, 10, 15, 20, 25, 30];
    return options.filter(x => x <= this.pagi.totalItem);
  }

  onPageSizeChange() {
    this.pagi.page = 1; // reset to first page
    this.getAllJobcard();
  }

  submitJobcard() {
    console.log("submitJobcard", this.jobcard);
    this.jobcardService.addJobcardWithVehicle(this.jobcard).subscribe(
      res => {
        this.alertStatus('Jobcard Added', 200);
        console.log(res);
        this.modalReference.close();
        this.getAllJobcard();
      },
      err => {
        this.alertStatus('something wrong', 404);
        console.log(err);
      }
    );
  }

  // update data
  updateJobcard() {
    console.log(this.jobcard);
    // http call
    this.jobcardService.editJobcardWithVehicle(this.jobcard).subscribe(
      res => {
        this.alertStatus('Jobcard Updated', 200);
        console.log(res);
        // close modal
        this.modalReference.close();
        this.getAllJobcard();
      },
      err => {
        this.alertStatus('something wrong', 404);
        console.log(err);
      }
    );
  }

  initEdit(content: any, data: JobCard) {
    this.alertStatus('', 0);
    this.extra.edit = true;

    // Deep clone
    this.jobcard = JSON.parse(JSON.stringify(data));

    // Safety checks
    if (!this.jobcard.vehicle) {
      this.jobcard.vehicle = new Vehicle();
    }

    if (!this.jobcard.jobCardStatus) {
      this.jobcard.jobCardStatus = new JobCardStatus();
    }

    this.modalReference = this.modalService.open(content, { size: 'lg' });
  }


}

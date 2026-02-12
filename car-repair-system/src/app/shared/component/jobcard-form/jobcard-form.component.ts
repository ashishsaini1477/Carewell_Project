import { Component, Input, OnInit } from '@angular/core';
import { JobCard, JobCardStatus } from '../../../pages/models/jobcard';
import { ValidationService } from '../../services/validation.service';
import { Customer, PagedResponse } from '../../../pages/models/customer';
import { CustomerService } from '../../../pages/services/customer.service';
import { FuelType } from '../../../pages/models/fuletype';

@Component({
  selector: 'jobcard-form',
  templateUrl: './jobcard-form.component.html',
  styleUrls: ['./jobcard-form.component.css']
})
export class JobcardFormComponent implements OnInit {

  public customerArray: Customer[] = [];
  public fuelTypeArray: FuelType[] = [];
  public jobcardStatusArray: JobCardStatus[] = [];
  isRegInvalid = false;


  @Input() jobcard: JobCard;
  constructor(
    public validation: ValidationService,
    private customerService: CustomerService,
  ) { }

  ngOnInit() {
    this.getAllCustomer();
    this.getFuelTypes();
    this.getJobcardStatus();
  }

  getAllCustomer() {
    this.customerService
      .getAllCustomer(0, 0)
      .subscribe(
        (res: PagedResponse<Customer>) => {
          // actual customer list
          this.customerArray = res.data;
          console.log('this.customerArray', this.customerArray);
        },
        err => console.log(err)
      );
  }
  getFuelTypes() {
    this.customerService
      .getFuelTypes()
      .subscribe(
        (res: FuelType[]) => {
          this.fuelTypeArray = res;
          console.log('this.fuelTypeArray', this.fuelTypeArray);
        },
        err => console.log(err)
      );
  }
  getJobcardStatus() {
    this.customerService
      .getJobcardStatus()
      .subscribe(
        (res: JobCardStatus[]) => {
          this.jobcardStatusArray = res;
          console.log('this.jobcardStatusArray', this.jobcardStatusArray);
        },
        err => console.log(err)
      );
  }

  onRegChange(value: string) {
    const reg = value.toUpperCase().replace(/\s/g, '');
    const indiaVehicleRegex = /^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{4}$/;
    this.isRegInvalid = !indiaVehicleRegex.test(reg);
  }

}

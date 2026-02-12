import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { Extra } from '../../models/extraModel';
import { CustomerService } from '../../services/customer.service';
import { CarService } from '../../services/car.service';
import { ValidationService } from '../../../shared/services/validation.service';
import { Router } from '@angular/router';
import { JobCard } from '../../models/jobcard';

@Component({
  selector: 'app-car-entrance',
  templateUrl: './car-entrance.component.html',
  styleUrls: ['./car-entrance.component.css']
})
export class CarEntranceComponent implements OnInit {

  public customerArray:Customer[] = [];
  public jobcard:JobCard;
  public extra:Extra;
  public cusReg:string = '';

  constructor(
    private customerService:CustomerService,
    private carService:CarService,
    public validation:ValidationService,
    private router:Router
  ) { 
    this.jobcard = new JobCard();
    this.extra = new Extra();
  }

  ngOnInit() {
    window.resizeTo(0,0);
    //this.getAllCustomer();
  }
  
  getAllCustomer(){
    this.customerService.getCustomer().subscribe(
      res =>{
        this.customerArray = this.validation.insertKeyInObject(res,'regNo');
      },
      err => console.log(err)
    );
  }

  
  private alertStatus(status,code){
    this.extra.code = code;
    this.extra.status = status;
  }
submitCar() {
  // TODO: add logic
}
}

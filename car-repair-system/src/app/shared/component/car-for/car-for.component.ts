import { Component, OnInit ,Input, EventEmitter, Output} from '@angular/core';
import { Car } from '../../../pages/models/car';
import { Customer, PagedResponse } from '../../../pages/models/customer';
import { CustomerService } from '../../../pages/services/customer.service';
import { JobCard } from '../../../pages/models/jobcard';

@Component({
  selector: 'car-form',
  templateUrl: './car-for.component.html',
  styleUrls: ['./car-for.component.css']
})
export class CarForComponent implements OnInit {

  @Input('car') car:Car;
  @Input('cusReg') cusReg:string;
  @Input('customerArray') customerArray:Customer[];
  @Output('changeCustomer') change:EventEmitter<string> = new EventEmitter();

    @Input() jobcard: JobCard;
public customers: Customer[] = [];
  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.getAllCustomer();
  }

  // changeCustomer(cus){
  //   this.change.emit(cus);
  // }
getAllCustomer() {
    this.customerService
      .getAllCustomer(0, 0)
      .subscribe(
        (res: PagedResponse<Customer>) => {
          // actual customer list
          this.customers = res.data;
          console.log('this.customers', this.customers);
        },
        err => console.log(err)
      );
  }
}

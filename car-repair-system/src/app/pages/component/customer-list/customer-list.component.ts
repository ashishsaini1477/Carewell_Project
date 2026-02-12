import { Component, OnInit } from '@angular/core';
import { Customer, PagedResponse } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';
import { Extra } from '../../models/extraModel';
import { Pagination } from '../../models/pagination';
import { ValidationService } from '../../../shared/services/validation.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { LoaderService } from '../../../shared/services/loader.service';



@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  public customer: Customer;
  private modalReference: NgbModalRef;
  public extra: Extra;
  public pagi: Pagination;
 prop: string = '';
 reverse: boolean = false;
  public customerArray: Customer[] = [];
  public filterCustomerArray: Customer[] = [];

  constructor(
    private modalService: NgbModal,
    public validation: ValidationService,
    private customerService: CustomerService,
    private router: Router,
    private loaderService: LoaderService
  ) {
    this.customer = new Customer();
    this.extra = new Extra();
    this.pagi = new Pagination();
  }

  ngOnInit() {
    window.resizeTo(0, 0);
    this.getAllCustomer();
  }

  getAllCustomer() {
    this.loaderService.show();
    this.customerService
      .getAllCustomer(this.pagi.page, this.pagi.itemPerPage)
      .subscribe(
        (res: PagedResponse<Customer>) => {

          // actual customer list
          this.loaderService.hide();
          this.customerArray = res.data;
          this.filterCustomerArray = res.data;

          this.pagi.totalItem = res.totalRecords;
          this.pagi.totalPages = Math.ceil(
            this.pagi.totalItem / this.pagi.itemPerPage
          );

          console.log('Customer List', res);
        },
        err => console.log(err)
      );
  }

  trackByCustomer(index: number, customer: Customer) {
    return customer.customerId;
  }




  // init add
  initAdd(content) {
    this.alertStatus('', 0);
    this.extra.edit = false;
    this.customer = new Customer();
    // open modal
    this.modalReference = this.modalService.open(content);
  }

  // submit form
  submitCustomer() {
    this.loaderService.show();
    console.log(this.customer);
    this.customerService.addCustomer(this.customer).subscribe(
      res => {
        this.extra.code = 200;
        this.extra.status = 'Customer Successfully Added'
        console.log(res);
        this.modalReference.close();
        this.getAllCustomer();
        this.loaderService.hide();
      },
      err => {
        this.extra.code = 404;
        this.extra.status = 'Something went wrong'
        console.log(err);
      }
    );
  }

  // init edit

  initEdit(content, data) {
    this.alertStatus('', 0);
    this.extra.edit = true;
    this.customer = new Customer();

    this.customer = data;
    // open modal
    this.modalReference = this.modalService.open(content);
  }

  // update data
  updateCustomer() {
    this.loaderService.show();
    console.log(this.customer);
    // http call
    this.customerService.updateCustomer(this.customer).subscribe(
      res => {
        this.loaderService.hide();
        this.alertStatus('Customer Updated', 200);
        console.log(res);
        // close modal
        this.modalReference.close();
      },
      err => {
        this.alertStatus('something wrong', 404);
        console.log(err);
      }
    );
  }

  // init archive
  initArchive(content, data) {
    this.alertStatus('', 0);
    this.extra.index = this.filterCustomerArray.indexOf(data);
    console.log(this.extra.index);
    this.customer = data;
    // open modal
    this.modalReference = this.modalService.open(content);
  }

  archiveCustomer() {
    console.log(this.customer);
    this.removeFromArray();
    // http call
    this.customerService.archiveCustomer(this.customer['regNo']).subscribe(
      res => {
        this.alertStatus('Customer Deleted', 200);
        console.log(res);
        // close modal
        this.modalReference.close();
      },
      err => {
        this.alertStatus('something wrong', 404);
        console.log(err);
      }
    );
  }

  private removeFromArray() {
    this.filterCustomerArray.splice(this.extra.index, 1);
    this.customerArray.splice(this.extra.index, 1);
  }

  private alertStatus(status, code) {
    this.extra.code = code;
    this.extra.status = status;
  }

  // sorting
 sortTable(property: string):void {
    if (this.prop === property) {
      this.reverse = !this.reverse;
    } else {
      this.prop = property;
      this.reverse = false;
    }
  }

  nextPage() {
    if (this.pagi.page < this.pagi.totalPages) {
      this.pagi.page++;
      this.getAllCustomer();
    }
  }

  prevPage() {
    if (this.pagi.page > 1) {
      this.pagi.page--;
      this.getAllCustomer();
    }
  }
  get pageSizeOptions(): number[] {
    const options = [5, 10, 15, 20, 25, 30];
    return options.filter(x => x <= this.pagi.totalItem);
  }

  onPageSizeChange() {
    this.pagi.page = 1; // reset to first page
    this.getAllCustomer();
  }

}

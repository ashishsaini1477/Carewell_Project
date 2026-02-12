import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';

@Injectable()
export class CustomerService {

  private path: string = 'customers';
  private masterPath: string = 'master';

  constructor(
    private http: HttpService
  ) { }

  getCustomer() {
    return this.http.get(this.path);
  }

  addCustomer(data) {
    return this.http.add(data, this.path);
  }

  updateCustomer(data) {
    let url = this.path;
    return this.http.update(data, url);
  }

  archiveCustomer(key) {
    let url = this.path + '/' + key;
    return this.http.archive(url);
  }

  joinDate() {
    let a = new Date();
    return a.getFullYear() + '-' +
      ((a.getMonth() + 1) > 9 ? (a.getMonth() + 1) : '0' + (a.getMonth() + 1)) + '-' +
      (a.getDate() > 9 ? a.getDate() : '0' + a.getDate());
  }

  getAllCustomer<T>(pageNumber: number, pageSize: number) {
    return this.http.getWithParam(
      this.path + '/allcustomer',
      {
        PageNumber: pageNumber,
        PageSize: pageSize
      }
    );
  }
  getDashboardCount() {
    return this.http.get(this.masterPath + "/count");
  }
  getFuelTypes() {
    return this.http.get(this.masterPath + "/fuel-types");
  }
  getJobcardStatus() {
    return this.http.get(this.masterPath + "/jobcard-status");
  }
  
   getCustomerAndVehicle(regNo:string) {
    let url = this.masterPath + '/getcustomer-vehicle/' + regNo;
    return this.http.get(url);
  }

  
}

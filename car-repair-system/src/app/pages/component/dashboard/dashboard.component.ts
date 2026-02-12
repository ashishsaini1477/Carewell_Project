import { Component, OnInit } from '@angular/core';
import { Customer, PagedResponse } from '../../models/customer';
import { DashboardCount } from '../../models/dashboardcount';
import { Car } from '../../models/car';
import { Stock } from '../../models/stock';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../../services/employee-service.service';
import { StockService } from '../../services/stock.service';
import { CarService } from '../../services/car.service';
import { CustomerService } from '../../services/customer.service';
import { DashboardService } from '../../services/dashboard.service';
import { Chart } from 'chart.js';
import { Router } from '@angular/router';
import { JobCard } from '../../models/jobcard';
import { JobcardService } from '../../services/jobcard.service';
import { LoaderService } from '../../../shared/services/loader.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public customerArray: Customer[] = [];
  carArray: Car[] = [];
  public stockArray: Stock[] = [];
  public stockArraySold: Stock[] = [];
  public empArray: Employee[] = [];
  public chart: Chart[] = [];
  public topFiveCus: Car[] = [];
   // Table sorting
  prop: string = '';
  reverse: boolean = false;

  public dashboardCount: DashboardCount = {
  customerCount: 0,
  jobCardCount: 0,
  vehicleCount: 0,
  invoiceCount:0
};
currentYear = new Date().getFullYear() - 1;

  public jobcardArray: JobCard[] = [];

  constructor(
    private customerService: CustomerService,
    private jobcardService: JobcardService,
    private carService: CarService,
    private stockService: StockService,
    private empService: EmployeeService,
    private dashboard: DashboardService,
    private router: Router,
    private loaderService: LoaderService
  ) { }

  ngOnInit() {
    this.getCustomer();
    this.getAllCustomer();
    this.getAllJobcard();
    // this.getEmployee();
  }

  getAllCustomer() {
    this.loaderService.show();
    this.customerService
      .getAllCustomer(1, 10)
      .subscribe(
        (res: PagedResponse<Customer>) => {
          // actual customer list
          this.loaderService.hide();
          this.customerArray = res.data;
          console.log('Customer List', res);
        },
        err => console.log(err)
      );
  }

  getCustomer() {
    this.loaderService.show();
    this.customerService.getDashboardCount().subscribe(
      (res: DashboardCount) => {
        this.dashboardCount = res != null ? res : new DashboardCount;
        console.log("dashboardCount", this.dashboardCount)
        this.loaderService.hide();
      },
      err => console.log(err)
    );
  }

  getAllJobcard() {
    this.loaderService.show();
    this.jobcardService.getalljobcords(1, 10).subscribe(
      (res: PagedResponse<JobCard>) => {
        this.loaderService.hide();
        this.jobcardArray = res.data;
      },
      err => console.log(err)
    );
  }

  getCar() {
    this.carService.getCar().subscribe(
      res => {
        let cars = this.dashboard.formatCars(res);
        this.carArray = cars['exist'];
        this.topFiveCus = this.dashboard.topFiveCustomer(Object.values(res || {}));
        this.doughnutChart(this.topFiveCus);
        this.lineChart();
      },
      err => console.log(err)
    );
  }

  getStock() {
    this.stockService.getStock().subscribe(
      res => {
        let obj = this.dashboard.formateStock(res);
        this.stockArraySold = this.dashboard.formatTopFiveSold(obj['sold']);
        this.stockArray = obj['nonSold'];
        this.barChart(this.stockArraySold);
        this.pieChart(this.stockArraySold);
      },
      err => console.log(err)
    );
  }

  getEmployee() {
    this.empService.getEmployee().subscribe(
      res => {
        this.empArray = Object.values(res);
      },
      err => console.log(err)
    );
  }

  barChart(data) {
    let bar = this.dashboard.formatBarChart(data);
    this.chart['bar'] = new Chart('canvasBar', bar);
  }

  pieChart(data) {
    let pie = this.dashboard.formatPieChart(data);
    this.chart['pie'] = new Chart('canvasPie', pie);
  }

  doughnutChart(data) {
    let dou = this.dashboard.formatDoughnutChart(data);
    this.chart['dou'] = new Chart('canvasDou', dou);
  }

  lineChart() {
    let dates = this.dashboard.creatRandomDates();
    let month = this.dashboard.formatMonthly(dates);
    let line = this.dashboard.formatLineChart(month);
    this.chart['line'] = new Chart('canvasLine', line);
  }

  goToAddCustomer() {
    this.router.navigate(['/add-customer']);
  }
  goToAddJobCard() {
    this.router.navigate(['/add-jobcard']);
  }
   goToAddInvoice() {
    this.router.navigate(['/add-invoice']);
  }
   goToEditInvoice() {
    this.router.navigate(['/edit-invoice']);
  }

   sortTable(prop: string): void {
    if (this.prop === prop) {
      this.reverse = !this.reverse; // toggle direction
    } else {
      this.prop = prop;
      this.reverse = false; // default ascending
    }

    this.carArray.sort((a: any, b: any) => {
      const x = a[prop];
      const y = b[prop];

      if (x == null) return 1;
      if (y == null) return -1;

      if (x > y) return this.reverse ? -1 : 1;
      if (x < y) return this.reverse ? 1 : -1;
      return 0;
    });
  }
}

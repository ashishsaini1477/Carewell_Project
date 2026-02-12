import { NgModule } from '@angular/core';
import { SharedModule } from './../shared/shared.module';
import { PagesRoutingModule } from './pages-routing.module';
import { PagesComponent } from './pages.component';
import { DashboardComponent } from './component/dashboard/dashboard.component';
import { CustomerAddComponent } from './component/customer-add/customer-add.component';
import { CustomerListComponent } from './component/customer-list/customer-list.component';
import { StockAddComponent } from './component/stock-add/stock-add.component';
import { CarEntranceComponent } from './component/car-entrance/car-entrance.component';
import { StockListComponent } from './component/stock-list/stock-list.component';
import { CarListComponent } from './component/car-list/car-list.component';
import { CarLeaveComponent } from './component/car-leave/car-leave.component';
import { GarageEmployeeListComponent } from './component/garage-employee-list/garage-employee-list.component';
import { EmployeeService } from './services/employee-service.service';

import {NgxPaginationModule} from 'ngx-pagination';
import { CustomerService } from './services/customer.service';
import { StockService } from './services/stock.service';
import { CarService } from './services/car.service';
import { SidenavDirective } from './directives/sidenav.directive';
import { DashboardService } from './services/dashboard.service';
import { JobcardAddComponent } from './component/jobcard-add/jobcard-add.component';
import { JobcardListComponent } from './component/jobcard-list/jobcard-list.component';
import { JobcardFormComponent } from '../shared/component/jobcard-form/jobcard-form.component';
import { JobcardService } from './services/jobcard.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InvoiceComponent } from './component/invoice/invoice.component';
import { InvoiceService } from './services/invoice.service';
import { EditInvoiceComponent } from './component/edit-invoice/edit-invoice.component';
import {  NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    FormsModule,
    PagesRoutingModule,
    NgxPaginationModule,
    NgbDropdownModule   
  ],
  declarations: [
    PagesComponent, 
    DashboardComponent, 
    CustomerAddComponent, 
    CustomerListComponent, 
    StockAddComponent, 
    CarEntranceComponent, 
    StockListComponent, 
    CarListComponent, 
    CarLeaveComponent, 
    GarageEmployeeListComponent, 
    SidenavDirective, JobcardAddComponent, JobcardListComponent, InvoiceComponent, EditInvoiceComponent
  ],
  providers: [
    EmployeeService,
    CustomerService,
    StockService,
    CarService,
    DashboardService,
    JobcardService,
    InvoiceService
  ]
})
export class PagesModule { }

export class CustomerReq {
    constructor(
       // public customerId:number = 0,
        public name:string = '',
        public mobile: number = 0,
        public address: string = '',
        public email: string ='',
    ){}
}

export class Customer {
  customerId: number = 0;
  name: string = '';
  mobile: number;
  address: string = '';        // ✅ MUST BE PRESENT
  email: string = '';
  isActive: boolean = false;
  createdDate?: Date;
    firstName?: string;
  lastName?: string;
}

export interface PagedResponse<T> {
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
  data: T[];
}
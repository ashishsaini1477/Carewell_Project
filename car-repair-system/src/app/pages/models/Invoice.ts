export class Invoice {
  jobCardId: number;
  invoiceId: number;
  customerId: number;
  invoiceDate: string;
  discountValue: number = 0;
  isStamp : boolean = false;
  grandTotal: number = 0;
  lineItems: InvoiceLineItem[] = [];
  labourWorks: LabourWork[] = [];
}

export class InvoiceLineItem {
  orderName: string;
  hsnSac?: string;
  qty: number = 1;
  unitPrice: number = 0;
  cgstPercent: number = 0;
  sgstPercent: number = 0;
  taxableAmount: number = 0;
  discount: number = 0;
  totalAmount: number = 0;
}
export class LabourWork {
  labourCharge: string;
  hsnSac?: string;
  qty: number = 1;
  unitPrice: number = 0;
  cgstPercent: number = 0;
  sgstPercent: number = 0;
  taxableAmount: number = 0;
  discount: number = 0;
  totalAmount: number = 0;
}

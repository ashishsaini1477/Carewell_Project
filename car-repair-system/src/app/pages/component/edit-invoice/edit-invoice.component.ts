import { Component, OnInit } from '@angular/core';
import { Invoice, InvoiceLineItem, LabourWork } from '../../models/Invoice';
import { JobcardAndCustomerWithVehicles } from '../../models/JobcardAndCustomerWithVehicles';
import { Extra } from '../../models/extraModel';
import { InvoiceService } from '../../services/invoice.service';
import { CustomerService } from '../../services/customer.service';
import { LoaderService } from '../../../shared/services/loader.service';
import { Router } from '@angular/router';

@Component({
  selector: 'edit-invoice',
  templateUrl: './edit-invoice.component.html',
  styleUrls: ['./edit-invoice.component.css']
})
export class EditInvoiceComponent implements OnInit {

  invoice: Invoice = new Invoice();
  invoiceList: Invoice[] = [];
  jobcardAndCustomerWithVehicles: JobcardAndCustomerWithVehicles[] = [];
  selectedJobCard: JobcardAndCustomerWithVehicles | null = null;
  registrationNumber: string = ''
  public extra: Extra;
  isRegInvalid: boolean = false;
  showJobCard = false;
  showInvoice = false;
  showInvoiceForm = false;


  constructor(private invoiceService: InvoiceService, private customerService: CustomerService,
    private loaderService: LoaderService, private router: Router) {
    this.extra = new Extra();
    this.registrationNumber = '';
  }

  ngOnInit() {

  }
  getCustomerWithVehicle() {
    this.loaderService.show();
    if (!this.registrationNumber || !this.registrationNumber.trim()) {
      this.extra.status = 'Please enter vehicle registration number';
      return;
    }
    this.customerService
      .getCustomerAndVehicle(this.registrationNumber)
      .subscribe(
        (res: JobcardAndCustomerWithVehicles[]) => {
          this.jobcardAndCustomerWithVehicles = res.map(j => ({
            ...j,
            openDate: new Date(j.openDate) as any
          }));
          this.loaderService.hide();
          console.log('jobcardAndCustomerWithVehicles for invoice', res);
          if (this.jobcardAndCustomerWithVehicles.length <= 0) {
            return alert("Your Job card status not completed")
          }
          this.showJobCard = true;
          this.showInvoice = false;
          this.showInvoiceForm = false;
        },
        err => console.log(err)
      );
  }


  addItem() {
    this.invoice.lineItems.push(new InvoiceLineItem());
  }


  addLabourItem() {
    this.invoice.labourWorks.push(new LabourWork());
  }

  calculate(item: InvoiceLineItem) {

    const qty = item.qty || 0;
    const price = item.unitPrice || 0;
    const discount = item.discount || 0;
    const cgstP = item.cgstPercent || 0;
    const sgstP = item.sgstPercent || 0;

    // 1️⃣ Base Amount
    const baseAmount = qty * price;

    // 2️⃣ Discount
    const discountAmount = baseAmount * discount / 100;
    const discountedAmount = baseAmount - discountAmount;

    // 3️⃣ GST
    const cgst = discountedAmount * cgstP / 100;
    const sgst = discountedAmount * sgstP / 100;

    // 4️⃣ Final values
    item.taxableAmount = +discountedAmount.toFixed(2);
    item.totalAmount = +(discountedAmount + cgst + sgst).toFixed(2);
  }

  calculateLabour(item: LabourWork) {

    const qty = item.qty || 0;
    const price = item.unitPrice || 0;
    const discount = item.discount || 0;
    const cgstP = item.cgstPercent || 0;
    const sgstP = item.sgstPercent || 0;

    const baseAmount = qty * price;

    const discountAmount = baseAmount * discount / 100;
    const discountedAmount = baseAmount - discountAmount;

    const cgst = discountedAmount * cgstP / 100;
    const sgst = discountedAmount * sgstP / 100;

    item.taxableAmount = +discountedAmount.toFixed(2);
    item.totalAmount = +(discountedAmount + cgst + sgst).toFixed(2);
  }

  get subTotal() {
    const itemTotal = this.invoice.lineItems
      .reduce((sum, i) => sum + (i.taxableAmount || 0), 0);

    const labourTotal = this.invoice.labourWorks
      .reduce((sum, i) => sum + (i.taxableAmount || 0), 0);

    return +(itemTotal + labourTotal).toFixed(2);
  }


  get grandTotal() {
    const itemTotal = this.invoice.lineItems
      .reduce((sum, i) => sum + (i.totalAmount || 0), 0);

    const labourTotal = this.invoice.labourWorks
      .reduce((sum, i) => sum + (i.totalAmount || 0), 0);

    const invoiceDiscount = this.invoice.discountValue || 0;

    return Math.max(itemTotal + labourTotal - invoiceDiscount, 0);
  }

  private toUtcDate(date: string): string {
  const d = new Date(date);
  return new Date(
    Date.UTC(d.getFullYear(), d.getMonth(), d.getDate())
  ).toISOString();
}


 private buildUpdateInvoicePayload() {
  return {
    invoiceId: this.invoice.invoiceId,
    invoiceDate: this.invoice.invoiceDate,
    discountValue: this.invoice.discountValue || 0,
    isStamp: this.invoice.isStamp || false,

    lineItems: this.invoice.lineItems.map(i => ({
      orderName: i.orderName,
      hsnSac: i.hsnSac,
      qty: i.qty,
      unitPrice: i.unitPrice,
      cgstPercent: i.cgstPercent,
      sgstPercent: i.sgstPercent,
      taxableAmount: i.taxableAmount,
      discount: i.discount,
      totalAmount: i.totalAmount
    })),

    labourWorks: this.invoice.labourWorks.map(l => ({
      labourCharge: l.labourCharge,
      hsnSac: l.hsnSac,
      qty: l.qty,
      unitPrice: l.unitPrice,
      cgstPercent: l.cgstPercent,
      sgstPercent: l.sgstPercent,
      taxableAmount: l.taxableAmount,
      discount: l.discount,
      totalAmount: l.totalAmount
    }))
  };
}


 editInvoice() {
  if (!this.isInvoiceValid()) {
    alert('Please fill all required invoice details');
    return;
  }

  const payload = this.buildUpdateInvoicePayload();
  console.log('UPDATE INVOICE PAYLOAD', payload); // ✅ DEBUG

  this.loaderService.show();

  this.invoiceService.updateInvoice(payload).subscribe({
    next: () => {
      alert('Invoice updated successfully');
      this.downloadPdf(this.invoice.invoiceId);
      this.loaderService.hide();
    },
    error: err => {
      this.loaderService.hide();
    }
  });
}



  downloadPdf(invoiceId: number) {
    this.invoiceService.createInvoicePDF(invoiceId)
      .subscribe(
        (blob: Blob) => {
          const url = window.URL.createObjectURL(blob);
          window.open(url);
          this.router.navigate(['/dashboard']);
        },
        err => alert('PDF download failed')
      );
  }

  onJobCardChange(jc: JobcardAndCustomerWithVehicles | null) {
    if (!jc || !jc.jobCardId) {
      return;
    }
    this.loaderService.show();

    // map values into invoice
    this.invoice.customerId = jc.customerId;
    this.invoice.jobCardId = jc.jobCardId;
    this.invoiceService.getInvoiceListByJobcardId(jc.jobCardId)
      .subscribe((res: Invoice[]) => {
        this.invoiceList = res;
        console.log('this.invoiceList', this.invoiceList);
        this.showInvoice = true;
        this.showInvoiceForm = false;
        this.loaderService.hide();
        err => alert(err.error.message || 'Invoice not found')
      });
  }

  removeItem(index: number) {
    this.invoice.lineItems.splice(index, 1);
  }
  removeLabourItem(index: number) {
    this.invoice.labourWorks.splice(index, 1);
  }

  calculateTotal() {
    let grandTotal = 0;

    this.invoice.lineItems.forEach(item => {

      // 1️⃣ Taxable Amount
      item.taxableAmount = item.qty * item.unitPrice;

      // 2️⃣ GST Calculation
      const cgstAmount =
        item.taxableAmount * (item.cgstPercent || 0) / 100;

      const sgstAmount =
        item.taxableAmount * (item.sgstPercent || 0) / 100;

      // 3️⃣ Line Total
      item.totalAmount =
        item.taxableAmount + cgstAmount + sgstAmount;

      // 4️⃣ Add to Invoice Total
      grandTotal += item.totalAmount;
    });

    // 5️⃣ Apply Discount
    const discount = this.invoice.discountValue || 0;
    this.invoice.grandTotal = grandTotal - discount;

    // Safety
    if (this.invoice.grandTotal < 0) {
      this.invoice.grandTotal = 0;
    }
  }

  onRegChange(value: string) {
    this.registrationNumber = value.toUpperCase().trim();
  }



  sanitizeNumber(item: any, field: string) {
    let value = Number(item[field]);

    // clear if empty / NaN
    if (isNaN(value)) {
      item[field] = null;
      return;
    }

    // ❌ remove negative numbers
    if (value < 0) {
      item[field] = null;
      return;
    }

    // ❌ auto-clear zero
    if (value === 0) {
      item[field] = null;
      return;
    }

    // ✅ qty should be integer
    if (field === 'qty') {
      item[field] = Math.floor(value);
    } else {
      item[field] = value;
    }
  }

  isInvoiceValid(): boolean {

    if (!this.invoice.jobCardId) {
      return false;
    }

    if (!this.invoice.invoiceDate) {
      return false;
    }

    if (
      (!this.invoice.lineItems || this.invoice.lineItems.length === 0) &&
      (!this.invoice.labourWorks || this.invoice.labourWorks.length === 0)
    ) {
      return false;
    }

    for (const item of this.invoice.lineItems) {
      if (!item.orderName || !item.qty) {
        return false;
      }
    }

    for (const item of this.invoice.labourWorks) {
      if (!item.labourCharge || !item.qty) {
        return false;
      }
    }

    if (this.grandTotal <= 0) {
      return false;
    }

    return true;
  }

  onInvoiceChange(data: Invoice) {
    this.showInvoiceForm = false;
    this.invoice = new Invoice();
    if (data) {
      this.invoice = data;
      if (this.invoice.invoiceDate) {
        this.invoice.invoiceDate = this.formatDate(this.invoice.invoiceDate);
      }
      this.showInvoiceForm = true;
      //this.calculateTotals();
    }
  }
  formatDate(date: string): string {
    const d = new Date(date);
    const year = d.getFullYear();
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

  getItemsTaxableTotal() {
    return this.invoice.lineItems
      .reduce((sum, x) => sum + (x.taxableAmount || 0), 0);
  }

  getItemsGrandTotal() {
    return this.invoice.lineItems
      .reduce((sum, x) => sum + (x.totalAmount || 0), 0);
  }

  getLabourTaxableTotal() {
    return this.invoice.labourWorks
      .reduce((sum, x) => sum + (x.taxableAmount || 0), 0);
  }

  getLabourGrandTotal() {
    return this.invoice.labourWorks
      .reduce((sum, x) => sum + (x.totalAmount || 0), 0);
  }

}

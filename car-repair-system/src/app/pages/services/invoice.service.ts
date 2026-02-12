import { Injectable } from '@angular/core';
import { Invoice } from '../models/Invoice';
import { HttpService } from '../../shared/services/http.service';

@Injectable()
export class InvoiceService {

  private path: string = 'Invoice';
  private masterPath: string = 'master';

  constructor(private http: HttpService) { }


  createInvoice(invoice: Invoice) {
    return this.http.add(invoice, this.path);
  }

  createInvoicePDF(invoiceId: number) {
    return this.http.getBlob(`${this.path}/${invoiceId}/pdf`);
  }

  updateInvoice(payload: any) {
    return this.http.update(payload,
      `${this.masterPath}/update-invoice`
    );
  }

  getInvoiceListByJobcardId(jobcardId: number) {
    return this.http.get(`${this.path}/${jobcardId}`);
  }
}

using AutoMapper;
using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Infrastructure.Constant;
using CarwellAutoshop.Infrastructure.Data;
using CarwellAutoshop.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarwellAutoshop.Infrastructure
{ 
    public class InvoiceData : IInvoiceData 
    {
        private readonly GarageDbContext _dbContext;
        private readonly IMapper _mapper;
        public InvoiceData(GarageDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateInvoice(InvoiceRequestDto request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var invoice = new Invoice
                {
                    JobCardId = request.JobCardId,
                    CustomerId = request.CustomerId,
                    InvoiceDate = request.InvoiceDate,
                    InvoiceNumber = InvoiceConst.InvoiceNumber, 
                    IsStamp = request.IsStamp,
                    DiscountValue = request.DiscountValue
                };

                decimal subTotal = 0;
                decimal cgst = 0;
                decimal sgst = 0;

                _dbContext.Invoice.Add(invoice);
                await _dbContext.SaveChangesAsync(); // InvoiceId generated

                foreach (var item in request.LineItems)
                {
                    var baseAmount = item.Qty * item.UnitPrice;
                    var discountPercent = item.Discount ?? 0;
                    var discountAmount = baseAmount * discountPercent / 100;

                    var taxable = baseAmount - discountAmount;

                    var cgstAmt = taxable * item.CGSTPercent / 100;
                    var sgstAmt = taxable * item.SGSTPercent / 100;

                    subTotal += taxable;
                    cgst += cgstAmt;
                    sgst += sgstAmt;

                    _dbContext.InvoiceLineItem.Add(new InvoiceLineItem
                    {
                        InvoiceId = invoice.InvoiceId,
                        OrderName = item.OrderName,
                        HsnSac = item.HsnSac,
                        Qty = item.Qty,
                        UnitPrice = item.UnitPrice,
                        Discount = discountPercent,
                        TaxableAmount = taxable,
                        CGSTPercent = item.CGSTPercent,
                        SGSTPercent = item.SGSTPercent,
                        TotalAmount = taxable + cgstAmt + sgstAmt
                    });
                }

                foreach (var item in request.LabourWorks)
                {
                    var baseAmount = item.Qty * item.UnitPrice;
                    var discountPercent = item.Discount ?? 0;
                    var discountAmount = baseAmount * discountPercent / 100;

                    var taxable = baseAmount - discountAmount;

                    var cgstAmt = taxable * item.CGSTPercent / 100;
                    var sgstAmt = taxable * item.SGSTPercent / 100;

                    subTotal += taxable;
                    cgst += cgstAmt;
                    sgst += sgstAmt;

                    _dbContext.LabourWork.Add(new LabourWork
                    {
                        InvoiceId = invoice.InvoiceId,
                        LabourCharge = item.LabourCharge,
                        HsnSac = item.HsnSac,
                        Qty = item.Qty,
                        UnitPrice = item.UnitPrice,
                        Discount = discountPercent,
                        TaxableAmount = taxable,
                        CGSTPercent = item.CGSTPercent,
                        SGSTPercent = item.SGSTPercent,
                        TotalAmount = taxable + cgstAmt + sgstAmt
                    });
                }


                invoice.SubTotal = subTotal;
                invoice.DiscountAmount = request.DiscountValue; // invoice-level discount
                invoice.CGST = cgst;
                invoice.SGST = sgst;
                invoice.GrandTotal = (subTotal + cgst + sgst) - request.DiscountValue;


                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return invoice.InvoiceId;

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }


        }

        public async Task<InvoiceResponse> GetInvoice(int id)
        {
            var invoice = await _dbContext.Invoice
                .Include(i => i.LineItems)
                .Include(i => i.LabourWorks)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
                return null;

            return _mapper.Map<InvoiceResponse>(invoice);
        }

        public async Task<IEnumerable<InvoiceResponse>> GetInvoiceByJobCardId(int jobcardId)
        {
            var invoices = await _dbContext.Invoice
                .Include(i => i.LineItems)
                .Include(i => i.LabourWorks)
                .Where(i => i.JobCardId == jobcardId).OrderByDescending(c=>c.InvoiceDate).ToListAsync();

            return _mapper.Map<List<InvoiceResponse>>(invoices);
        }
    }
}

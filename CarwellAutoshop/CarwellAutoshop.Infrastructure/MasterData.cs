using AutoMapper;
using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Domain.Enums;
using CarwellAutoshop.Infrastructure.Constant;
using CarwellAutoshop.Infrastructure.Data;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CarwellAutoshop.Infrastructure
{
    public class MasterData : IMasterData
    {
        private readonly GarageDbContext _dbContext;
        private readonly IMapper _mapper;

        public MasterData(GarageDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FuelTypeResponse>> GetFuelTypeAsync()
        {
            var result = await _dbContext.FuelType.ToListAsync();
            return _mapper.Map<List<FuelTypeResponse>>(result);
        }

        public async Task<IEnumerable<JobCardStatusResponse>> GetJobCardStatusAsync()
        {
            var result = await _dbContext.JobCardStatus.ToListAsync();
            return _mapper.Map<List<JobCardStatusResponse>>(result);
        }

        public async Task<IEnumerable<PaymentModeResponse>> GetPaymentModeAsync()
        {
            var result = await _dbContext.PaymentMode.ToListAsync();
            return _mapper.Map<List<PaymentModeResponse>>(result);
        }
        public async Task<DashboardResponse> GetDashboardCount()
        {
            int customerCount = await _dbContext.Customer.CountAsync();
            int jobCount = await _dbContext.JobCard.CountAsync();
            int vehicleCount = await _dbContext.Vehicle.CountAsync();
            int invoiceCount = await _dbContext.Invoice.CountAsync();
            return new DashboardResponse
            {
                CustomerCount = customerCount,
                JobCardCount = jobCount,
                VehicleCount = vehicleCount,
                InvoiceCount = invoiceCount
            };
        }
        public async Task<IEnumerable<JobcardAndCustomerWithVehiclesResponse>> GetCustomerByRegistrationNo(string registrationNo)
        {
            var result = await
        (from v in _dbContext.Vehicle
         join c in _dbContext.Customer on v.CustomerId equals c.CustomerId
         join jc in _dbContext.JobCard on v.VehicleId equals jc.VehicleId
         join jcs in _dbContext.JobCardStatus on jc.JobCardStatusId equals jcs.JobCardStatusId
         where v.RegistrationNo == registrationNo && jc.JobCardStatusId == (int)JobCardStatusEnum.Completed
         orderby jc.OpenDate descending
         select new JobcardAndCustomerWithVehiclesResponse
         {
             // 🔹 Customer
             CustomerId = c.CustomerId,
             Name = c.Name,
             Mobile = c.Mobile,

             // 🔹 Vehicle
             VehicleId = v.VehicleId,
             Vehicles = new List<VehicleResponse>
             {
                 new VehicleResponse
                 {
                     VehicleId = v.VehicleId,
                     RegistrationNo = v.RegistrationNo,
                     Model = v.Model
                 }
             },

             // 🔹 JobCard
             JobCardId = jc.JobCardId,
             GarageId = jc.GarageId,
             JobCardStatusId = jc.JobCardStatusId,
             StatusName = jcs.StatusName,
             OdometerReading = jc.OdometerReading,
             OpenDate = jc.OpenDate
         })
        .AsNoTracking()
        .ToListAsync();

            return result;
        }

        public async Task<InvoicePdfResponse> GetInvoicePdf(int invoiceId)
        {
            try
            {
                var invoice = _dbContext.Invoice.AsNoTracking().FirstOrDefault(i => i.InvoiceId == invoiceId);


                if (invoice == null)
                    return null;

                var customerObj = await _dbContext.Customer.AsNoTracking()
               .FirstOrDefaultAsync(i => i.CustomerId == invoice.CustomerId);

                var invoiceLineItems = await _dbContext.InvoiceLineItem.AsNoTracking()
                 .Where(x => x.InvoiceId == invoice.InvoiceId).ToListAsync();

                var invoiceLabourWorks = await _dbContext.LabourWork.AsNoTracking()
                    .Where(x => x.InvoiceId == invoice.InvoiceId).ToListAsync();



                var jobCardObj = await _dbContext.JobCard.AsNoTracking()
               .FirstOrDefaultAsync(i => i.JobCardId == invoice.JobCardId);

                var garageObj = await _dbContext.Garage.AsNoTracking()
               .FirstOrDefaultAsync(i => i.GarageId == (int)GarageEnum.Carwell);

                JobCardRemark remarkObj = null;
                Vehicle vehicleObj = null;

                if (jobCardObj != null)
                {
                    vehicleObj = new Vehicle();
                    vehicleObj = await _dbContext.Vehicle.AsNoTracking().FirstOrDefaultAsync(i => i.VehicleId == jobCardObj.VehicleId);

                    var remark = await _dbContext.JobCardRemark.AsNoTracking()
                                .Where(i => i.JobCardId == jobCardObj.JobCardId).OrderByDescending(v => v.CreatedDate).FirstOrDefaultAsync();
                    if (remark != null)
                    {
                        remarkObj = new JobCardRemark();
                        remarkObj = remark;
                    }
                }


                var response = new InvoicePdfResponse
                {
                    // 🏢 Garage details
                    GarageName = garageObj?.Name?.ToUpper(),
                    GarageAddress = garageObj?.Address,
                    GaragePhone = garageObj?.Phone,
                    GstNumber = garageObj?.GSTIN,

                    // 👤 Customer info
                    CustomerName = customerObj?.Name?.ToUpper(),
                    CustomerMobile = Convert.ToString(customerObj?.Mobile),
                    CustomerAddress = customerObj?.Address?.ToUpper(),

                    // 🚗 Vehicle info
                    VehicleNumber = vehicleObj?.RegistrationNo?.ToUpper(),
                    VehicleModel = vehicleObj?.Model?.ToUpper(),
                    Odometer = jobCardObj?.OdometerReading,

                    // 🧾 Invoice info
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceDate = invoice.InvoiceDate,
                    IsStamp = invoice.IsStamp,
                    Remarks = remarkObj?.RemarkText,
                    PaymentMode = InvoiceConst.Cash,

                    // 📦 Line Items
                    Items = invoiceLineItems.Select(item => new InvoiceLineItemResponse
                    {
                        LineItemId = item.LineItemId,
                        OrderName = item.OrderName,
                        HsnSac = item.HsnSac,
                        Qty = item.Qty,
                        UnitPrice = item.UnitPrice,
                        CGSTPercent = item.CGSTPercent,
                        SGSTPercent = item.SGSTPercent,
                        TaxableAmount = item.TaxableAmount,
                        TotalAmount = item.TotalAmount
                    }).ToList(),

                    // 🛠 Labour Works
                    LabourWorks = invoiceLabourWorks.Select(item => new LabourWorkResponse
                    {
                        LabourWorkId = item.LabourWorkId,
                        LabourCharge = item.LabourCharge,
                        HsnSac = item.HsnSac,
                        Qty = item.Qty,
                        UnitPrice = item.UnitPrice,
                        CGSTPercent = item.CGSTPercent,
                        SGSTPercent = item.SGSTPercent,
                        TaxableAmount = item.TaxableAmount,
                        TotalAmount = item.TotalAmount
                    }).ToList(),

                    // 💰 Totals
                    SubTotal = invoice.SubTotal,
                    Discount = invoice.DiscountAmount,
                    CGST = invoice.CGST,
                    SGST = invoice.SGST,
                    GrandTotal = invoice.GrandTotal,
                    AmountInWords = NumberToWordsConverter.Convert(invoice.GrandTotal)
                };



                return response;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> UpdateInvoice(UpdateInvoiceRequestDto request)
        {
           // using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var invoice = await _dbContext.Invoice
                    .FirstOrDefaultAsync(x => x.InvoiceId == request.InvoiceId);

                var invoiceLineItems = await _dbContext.InvoiceLineItem
                   .Where(x => x.InvoiceId == request.InvoiceId).ToListAsync();

                var invoiceLabourWorks = await _dbContext.LabourWork
                    .Where(x => x.InvoiceId == request.InvoiceId).ToListAsync();

                if (invoice == null)
                    throw new Exception("Invoice not found");

                // Update master
                invoice.InvoiceDate = request.InvoiceDate;
                invoice.DiscountAmount = request.DiscountValue;
                invoice.IsStamp = request.IsStamp;
                invoice.DiscountValue = request.DiscountValue; 

                // Remove old items
                _dbContext.InvoiceLineItem.RemoveRange(invoiceLineItems);
                _dbContext.LabourWork.RemoveRange(invoiceLabourWorks);

                // flush deletes so DB-assigned keys / constraints are processed before new inserts
                await _dbContext.SaveChangesAsync();


                decimal subTotal = 0;
                decimal cgst = 0;
                decimal sgst = 0;

                // now add updated line items
                foreach (var item in request.LineItems)
                {
                    var taxable = item.Qty * item.UnitPrice;
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
                        TaxableAmount = taxable,
                        CGSTPercent = item.CGSTPercent,
                        SGSTPercent = item.SGSTPercent,
                        Discount = item.Discount,
                        TotalAmount = taxable + cgstAmt + sgstAmt
                    });
                }

                // Add updated labour works
                foreach (var item in request.LabourWorks)
                {
                    var taxable = item.Qty * item.UnitPrice;
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
                        TaxableAmount = taxable,
                        CGSTPercent = item.CGSTPercent,
                        SGSTPercent = item.SGSTPercent,
                        Discount = item.Discount,
                        TotalAmount = taxable + cgstAmt + sgstAmt
                    });
                }

                // Totals
                invoice.SubTotal = subTotal;
                invoice.CGST = cgst;
                invoice.SGST = sgst;
                invoice.GrandTotal = (subTotal + cgst + sgst) - request.DiscountValue;

                await _dbContext.SaveChangesAsync();
                //await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                throw ex;
            }
        }


    }
}

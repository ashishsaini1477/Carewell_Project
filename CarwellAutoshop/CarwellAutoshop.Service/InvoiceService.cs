using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Service.Interface;

namespace CarwellAutoshop.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceData _invoiceData;
        public InvoiceService(IInvoiceData invoiceData) 
        {
            _invoiceData = invoiceData;
        }
        public async Task<int> CreateInvoice(InvoiceRequestDto request)
        {
            return await _invoiceData.CreateInvoice(request);
        }
        public async Task<InvoiceResponse> GetInvoice(int id)
        {
            return await _invoiceData.GetInvoice(id);
        }

        public async Task<IEnumerable<InvoiceResponse>> GetInvoiceByJobCardId(int jobcardId)
        {
            return await _invoiceData.GetInvoiceByJobCardId(jobcardId);
        }
        
    }
}

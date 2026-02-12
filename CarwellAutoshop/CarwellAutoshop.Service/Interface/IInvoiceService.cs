using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Service.Interface
{
    public interface IInvoiceService
    {
        Task<int> CreateInvoice(InvoiceRequestDto request);
        Task<InvoiceResponse> GetInvoice(int id);
        Task<IEnumerable<InvoiceResponse>> GetInvoiceByJobCardId(int jobcardId);
    }
}

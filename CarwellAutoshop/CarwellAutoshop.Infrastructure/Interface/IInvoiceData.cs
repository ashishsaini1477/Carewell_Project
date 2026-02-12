using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Infrastructure.Interface
{
    public interface IInvoiceData
    {
        Task<int> CreateInvoice(InvoiceRequestDto request);
        Task<InvoiceResponse> GetInvoice(int id);
        Task<IEnumerable<InvoiceResponse>> GetInvoiceByJobCardId(int jobcardId);
    }
}

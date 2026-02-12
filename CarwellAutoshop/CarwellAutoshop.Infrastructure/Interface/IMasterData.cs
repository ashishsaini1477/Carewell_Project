using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Infrastructure.Interface
{
    public interface IMasterData
    {
        Task<IEnumerable<FuelTypeResponse>> GetFuelTypeAsync();
        Task<IEnumerable<JobCardStatusResponse>> GetJobCardStatusAsync();
        Task<IEnumerable<PaymentModeResponse>> GetPaymentModeAsync();
        Task<DashboardResponse> GetDashboardCount();
        Task<IEnumerable<JobcardAndCustomerWithVehiclesResponse>> GetCustomerByRegistrationNo(string registrationNo);
        Task<InvoicePdfResponse> GetInvoicePdf(int invoiceId);
        Task<bool> UpdateInvoice(UpdateInvoiceRequestDto request);
    }
}

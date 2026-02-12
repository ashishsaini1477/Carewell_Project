using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Service.Interface;

namespace CarwellAutoshop.Service
{
    public class MasterService : IMasterService
    {
        private readonly IMasterData _masterData;
        public MasterService(IMasterData masterData)
        {
            _masterData = masterData;
        }
        public async Task<IEnumerable<FuelTypeResponse>> GetFuelTypeAsync()
        {
            return await _masterData.GetFuelTypeAsync();
        }

        public async Task<IEnumerable<JobCardStatusResponse>> GetJobCardStatusAsync()
        {
            return await _masterData.GetJobCardStatusAsync();
        }

        public async Task<IEnumerable<PaymentModeResponse>> GetPaymentModeAsync()
        {
            return await _masterData.GetPaymentModeAsync();
        }

        public async Task<DashboardResponse> GetDashboardCount()
        {
            return await _masterData.GetDashboardCount();
        }
        public async Task<IEnumerable<JobcardAndCustomerWithVehiclesResponse>> GetCustomerByRegistrationNo(string registrationNo)
        {
            return await _masterData.GetCustomerByRegistrationNo(registrationNo);
        }
        public async Task<InvoicePdfResponse> GetInvoicePdf(int invoiceId)
        {
            return await _masterData.GetInvoicePdf(invoiceId);
        }
        public async Task<bool> UpdateInvoice(UpdateInvoiceRequestDto request)
        {
            return await _masterData.UpdateInvoice(request);
        }
    }
}

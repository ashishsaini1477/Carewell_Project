namespace CarwellAutoshop.Domain.DTOs.Response
{
    public class PaymentModeResponse
    {
        public int PaymentModeId { get; set; }

        public string ModeName { get; set; }

        public bool IsOnline { get; set; }

        public bool IsActive { get; set; }
    }
}

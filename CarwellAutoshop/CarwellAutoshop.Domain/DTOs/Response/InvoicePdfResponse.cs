namespace CarwellAutoshop.Domain.DTOs.Response
{
    public class InvoicePdfResponse
    {
        public string GarageName { get; set; }
        public string GarageAddress { get; set; }
        public string GaragePhone { get; set; }
        public string GstNumber { get; set; }

        public string CustomerName { get; set; }
        public string? CustomerMobile { get; set; }
        public string? CustomerAddress { get; set; }
        public bool IsStamp { get; set; }

        public string VehicleNumber { get; set; }
        public string VehicleModel { get; set; }
        public int? Odometer { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<InvoiceLineItemResponse> Items { get; set; }
        public List<LabourWorkResponse> LabourWorks { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal GrandTotal { get; set; }

        public string PaymentMode { get; set; }
        public string AmountInWords { get; set; }
        public string? Remarks { get; set; }
    }
}

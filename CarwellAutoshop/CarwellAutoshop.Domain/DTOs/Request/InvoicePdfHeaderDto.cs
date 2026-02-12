namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class InvoicePdfHeaderDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public bool IsStamp { get; set; }

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal GrandTotal { get; set; }

        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }

        public string VehicleNumber { get; set; }
        public string VehicleModel { get; set; }
        public int? OdometerReading { get; set; }

        public string GarageName { get; set; }
        public string GarageAddress { get; set; }
        public string GaragePhone { get; set; }
        public string GSTIN { get; set; }

        public string LatestRemark { get; set; }
    }

}

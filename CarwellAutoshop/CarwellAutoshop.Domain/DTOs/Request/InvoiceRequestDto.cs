namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class InvoiceRequestDto
    {
        public int JobCardId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal DiscountValue { get; set; }
        public int? DiscountTypeId { get; set; }
        public bool IsStamp { get; set; }

        public List<InvoiceLineItemDto> LineItems { get; set; }
        public List<LabourWorkDto> LabourWorks { get; set; }
    }
    public class InvoiceLineItemDto : InvoiceCommon
    {
        public string OrderName { get; set; }
      
    }
    public class LabourWorkDto : InvoiceCommon
    {
        public string LabourCharge { get; set; }
    }

    public class InvoiceCommon
    {
        public string? HsnSac { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CGSTPercent { get; set; }
        public decimal SGSTPercent { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }

}

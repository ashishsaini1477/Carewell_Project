namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class UpdateInvoiceRequestDto
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsStamp { get; set; }

        public List<InvoiceLineItemDto> LineItems { get; set; }
        public List<LabourWorkDto> LabourWorks { get; set; }
    }

}

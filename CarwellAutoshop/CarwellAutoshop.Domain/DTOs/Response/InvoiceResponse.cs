using CarwellAutoshop.Domain.DTOs.Request;

namespace CarwellAutoshop.Domain.DTOs.Response
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }
        public int JobCardId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CGST { get; set; } 
        public decimal SGST { get; set; }
        public decimal RoundOff { get; set; }
        public decimal GrandTotal { get; set; }
        public List<InvoiceLineItemResponse> LineItems { get; set; }
        public List<LabourWorkResponse> LabourWorks { get; set; }
    }
    public class InvoiceLineItemResponse : InvoiceCommon
    {
        public int LineItemId { get; set; }
        public string OrderName { get; set; }
    }
    public class LabourWorkResponse : InvoiceCommon
    {
        public int LabourWorkId { get; set; }
        public string LabourCharge { get; set; }
    }

}

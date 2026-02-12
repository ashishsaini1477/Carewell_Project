namespace CarwellAutoshop.Domain.Entities
{
    public class LabourWork
    {
        public int LabourWorkId { get; set; }
        public int InvoiceId { get; set; }

        public string LabourCharge { get; set; }
        public string? HsnSac { get; set; }

        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TaxableAmount { get; set; }
        public decimal CGSTPercent { get; set; }
        public decimal SGSTPercent { get; set; }

        public decimal? Discount { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation
        //public Invoice Invoice { get; set; }
    }
}

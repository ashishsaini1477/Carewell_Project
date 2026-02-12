using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class InvoiceLineItem
    {
        [Key]
        public int LineItemId { get; set; }

        public int InvoiceId { get; set; }
        public string OrderName { get; set; }
        public string? HsnSac { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxableAmount { get; set; }

        public decimal CGSTPercent { get; set; }
        public decimal SGSTPercent { get; set; }
        public decimal? Discount { get; set; }

        public decimal TotalAmount { get; set; }

       // public Invoice Invoice { get; set; }
    }
}

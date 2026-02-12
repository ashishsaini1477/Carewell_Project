using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("labourwork")]
    public class LabourWork
    {
        [Column("labourworkid")]
        public int LabourWorkId { get; set; }

        [Column("invoiceid")]
        public int InvoiceId { get; set; }

        [Column("labourcharge")]
        public string LabourCharge { get; set; }

        [Column("hsnsac")]
        public string? HsnSac { get; set; }

        [Column("qty")]
        public int Qty { get; set; }

        [Column("unitprice")]
        public decimal UnitPrice { get; set; }

        [Column("taxableamount")]
        public decimal TaxableAmount { get; set; }

        [Column("cgstpercent")]
        public decimal CGSTPercent { get; set; }

        [Column("sgstpercent")]
        public decimal SGSTPercent { get; set; }

        [Column("discount")]
        public decimal? Discount { get; set; }

        [Column("totalamount")]
        public decimal TotalAmount { get; set; }

        // Navigation
        //public Invoice Invoice { get; set; }
    }
}
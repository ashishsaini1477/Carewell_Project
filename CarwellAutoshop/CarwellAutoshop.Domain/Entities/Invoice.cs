using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public int JobCardId { get; set; }
        public int CustomerId { get; set; }

        [Required, MaxLength(50)]
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public bool IsStamp { get; set; }
        public decimal RoundOff { get; set; }
        public decimal GrandTotal { get; set; }
        
       // public JobCard JobCard { get; set; }
        // Customer Customer { get; set; }

        public ICollection<InvoiceLineItem> LineItems { get; set; }
        public ICollection<LabourWork> LabourWorks { get; set; }
    }

}

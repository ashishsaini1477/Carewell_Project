using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("invoice")]
    public class Invoice
    {
        [Key]
        [Column("invoiceid")]
        public int InvoiceId { get; set; }

        [Column("jobcardid")]
        public int JobCardId { get; set; }

        [Column("customerid")]
        public int CustomerId { get; set; }

        [Required, MaxLength(50)]
        [Column("invoicenumber")]
        public string InvoiceNumber { get; set; }

        [Column("invoicedate")]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        [Column("subtotal")]
        public decimal SubTotal { get; set; }

        [Column("discountamount")]
        public decimal DiscountAmount { get; set; }

        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }

        [Column("discounttypeid")]
        public int? DiscountTypeId { get; set; }

        [Column("cgst")]
        public decimal CGST { get; set; }

        [Column("sgst")]
        public decimal SGST { get; set; }

        [Column("isstamp")]
        public bool IsStamp { get; set; }

        [Column("roundoff")]
        public decimal RoundOff { get; set; }

        [Column("grandtotal")]
        public decimal GrandTotal { get; set; }

        // Navigation and collections
        // public JobCard JobCard { get; set; }
        // public Customer Customer { get; set; }

        public ICollection<InvoiceLineItem> LineItems { get; set; }
        public ICollection<LabourWork> LabourWorks { get; set; }
    }
}